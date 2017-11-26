using AutoMapper;
using Chicadresse.Business.Services;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IUserActivationService _userActivationService;
        private readonly ITaskTimingService _taskTimingService;
        private readonly ITaskService _taskService;
        private readonly IUserTaskService _userTaskService;

        #endregion

        #region ctor

        public LoginController(IUserService userService, IUserActivationService userActivationService, ITaskTimingService taskTimingService, ITaskService taskService, IUserTaskService userTaskService)
        {
            this._userService = userService;
            this._userActivationService = userActivationService;
            this._taskTimingService = taskTimingService;
            this._taskService = taskService;
            this._userTaskService = userTaskService;
        }

        #endregion

        #region Actions

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel objUser)
        {
            if (objUser != null)
            {
                var obj = this._userService.GetUser(objUser.Email, objUser.Password);
                if (obj != null && obj.ActivationStatus == true)
                {
                    var user = Mapper.Map<User, UserViewModel>(obj); // obj is of type user and id mapped to user i.e., userviewmodel
                    System.Web.HttpContext.Current.Session["userSession"] = user;
                    return RedirectToAction("Index", "Dashboard", obj);
                }
                else
                {
                    return Redirect("/Login/Index#accNotActivated");
                }
            }

            return View(objUser);
        }

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        public ActionResult Signup(UserViewModel obj, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    //Check if email exist
                    var emailexist = _userService.GetUserByEmail(obj.Email);
                    if (emailexist != null)
                    {
                        ModelState.AddModelError("Email", "Email address already exists.");
                        return View();
                    }
                    else
                    {
                        var radio = Convert.ToString(form["credit-card"].ToString());
                        obj.User_Id = 1;
                        if (radio == "Groom")
                            obj.User_Id = 2;
                        obj.ActivationStatus = false;
                        var userModel = Mapper.Map<UserViewModel, User>(obj);

                        _userService.AddUser(userModel);
                        TempData["UserData"] = userModel;
                        SendActivationEmail(userModel);
                        return Redirect("/Login/Signup#registered");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignupModal(UserViewModel obj, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var user = (User)TempData["UserData"];

                User data = _userService.GetUser(user.Email, user.Password);

                data.Budget = obj.Budget;
                data.NoOfGuest = obj.NoOfGuest;
                data.PartnerName = obj.PartnerName;

                _userService.UpdateUser(data);

                return Redirect("/Login/Index#alert");
            }
            return View();
        }

        // Sending confirm email to user
        private void SendActivationEmail(User obj)
        {
            Guid activationCode = Guid.NewGuid();

            using (MailMessage mm = new MailMessage("latikasood.in@gmail.com", obj.Email))
            {
                mm.Subject = "Confirm your email";
                string body = "Hello " + obj.Name + ",";
                body += "<br /><br />One more click and you're ready to go.<br />We are sending you this email so you can confirm your registration.";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Login/Activation?code={2}&email={3}", Request.Url.Scheme, Request.Url.Authority, activationCode, obj.Email) + "'>Confirm my email</a>";
                body += "<br /><br />If you're not signed up with Chicadresse and received this email by error, ignore it and our apologies for the inconvenience.<br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("latikasood.in@gmail.com", "aneja1991");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }

        // On click of confirm email activating the account
        public ActionResult Activation()
        {
            Guid activationCode = new Guid(Request.QueryString["code"].ToString());
            var email = Request.QueryString["email"].ToString();

            if (activationCode != null)
            {
                var obj = _userService.GetUserByEmail(email);
                obj.ActivationStatus = true;

                User_Activation userActivation = new User_Activation();
                userActivation.ActivationCode = activationCode;
                userActivation.UserId = obj.Id;
                _userActivationService.AddUser(userActivation);

                _userService.UpdateUser(obj);

                var marriageDate = Convert.ToDateTime(obj.MarriageDate);
                var currentDate = DateTime.Now;
                var monthBetweenMarriage = (marriageDate.Month - currentDate.Month) + 12 * (marriageDate.Year - currentDate.Year);
                List<int> list = new List<int>();
                for (int i = 1; i <= monthBetweenMarriage; i++)
                    list.Add(i);

                IEnumerable<Task_Timing> timeInMonthList = _taskTimingService.GetTask();

                HashSet<int> diffids = new HashSet<int>(list);
                var timeInMonth1 = timeInMonthList.Where(m => diffids.Contains(m.Timing));
                HashSet<int> timeInMonth = new HashSet<int>(timeInMonth1.Select(s => s.Timing));

                IEnumerable<Task> tskList = _taskService.GetTaskByTimeMonth(timeInMonth);

                IEnumerable<User_Task> usertskList = tskList.Select(x => new User_Task { UserId = x.UserId.HasValue ? x.UserId.Value : obj.Id, TaskId = x.TaskId, CompletionStatus = x.CompletionStatus.HasValue ? x.CompletionStatus.Value : false });

                _userTaskService.Add(usertskList);

                return RedirectToAction("Index", "Dashboard", obj);
            }
            return View();
        }

        // Forget Password
        public ActionResult ForgetPassword()
        {
            return View();
        }

        // POST: Forget Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword(FormCollection form)
        {
            var email = Convert.ToString(form["email"].ToString());
            ViewBag.Email = email;
            if (email != null)
            {
                var obj = _userService.GetUserByEmail(email);
                if (obj != null)
                {
                    using (MailMessage mm = new MailMessage("latikasood.in@gmail.com", obj.Email))
                    {
                        mm.Subject = "Recover your password";
                        string body = "Hello " + obj.Name + ",";
                        body += "<br /><br />Recover password<br />Don't worry if you forgot it, it happens to all of us.<br />Click here to create a new password.";
                        body += "<br /><a href = '" + string.Format("{0}://{1}/Login/NewPassword/?email={2}", Request.Url.Scheme, Request.Url.Authority, email) + "'>Create a new password</a>";
                        body += "<br /><br />If you're not signed up with Chicadresse and received this email by error, ignore it and our apologies for the inconvenience.<br />Thanks";
                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("latikasood.in@gmail.com", "aneja1991");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
                return Redirect("/Login/ForgetPassword#emailnotexist");
            }
            return Redirect("/Login/ForgetPassword#alert");
        }

        // New Password
        public ActionResult NewPassword()
        {
            return View();
        }

        // POST: Forget Password
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPassword(FormCollection form)
        {
            var email = Request.QueryString["email"].ToString();
            var newPasswd = Convert.ToString(form["NewPassword"].ToString());

            User user = _userService.GetUserByEmail(email);
            if (user != null)
            {
                user.Password = newPasswd;
                _userService.UpdateUser(user);

                return RedirectToAction("Index", "Dashboard", user);
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["userSession"] = null;
            System.Web.HttpContext.Current.Session["userEmail"] = null;
            return RedirectToAction("Index", "Login");
        }

        #endregion
    }
}