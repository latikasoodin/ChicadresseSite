using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.UnitOfWork;
using DAL;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;

namespace ChicadresseSite.Controllers
{
    public class LoginController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (objUser != null)
            {
                var obj = unitOfWork.UserRepository.Get().Where(a => a.Email.Equals(objUser.Email) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    System.Web.HttpContext.Current.Session["userSession"] = obj;
                    System.Web.HttpContext.Current.Session["userEmail"] = obj.Email;
                    return RedirectToAction("Index", "Dashboard", obj);
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
        public ActionResult Signup(User obj, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (obj != null)
                {
                    var radio = Convert.ToString(form["credit-card"].ToString());
                    obj.User_Id = 1;
                    if (radio == "Groom")
                        obj.User_Id = 2;
                    unitOfWork.UserRepository.Insert(obj);
                    unitOfWork.Save();
                    var verified = SendActivationEmail(obj);
                    if (verified == "success")
                    {
                        ////To add by default first 29 tasks taskId to User_Task table
                        //User_Task usrTsk = new User_Task();
                        //IEnumerable<Task> tskList = unitOfWork.TaskRepository.Get().Take(29);

                        //IEnumerable<User_Task> usertskList = tskList.Select(x => new User_Task { UserId = x.UserId.HasValue ? x.UserId.Value : obj.Id, TaskId = x.TaskId, CompletionStatus = x.CompletionStatus.HasValue ? x.CompletionStatus.Value : false });

                        //unitOfWork.UserTaskRepository.Insert(usertskList);
                        //unitOfWork.Save();

                        var marriageDate = Convert.ToDateTime(obj.MarriageDate);
                        var currentDate = DateTime.Now;
                        var monthBetweenMarriage = (marriageDate.Month - currentDate.Month) + 12 * (marriageDate.Year - currentDate.Year);
                        List<int> list = new List<int>();
                        for (int i = 1; i <= monthBetweenMarriage; i++)
                            list.Add(i);

                        IEnumerable<Task_Timing> timeInMonthList = unitOfWork.TaskTimingRepository.Get();

                        HashSet<int> diffids = new HashSet<int>(list);
                        //You will have the difference here
                        var timeInMonth1 = timeInMonthList.Where(m => diffids.Contains(m.Timing));
                        HashSet<int> timeInMonth = new HashSet<int>(timeInMonth1.Select(s => s.Timing));
                        //HashSet<int> timeInMonth = new HashSet<int>(timeInMonthList.Select(s => s.TimingId).Where(s => s.));

                        IEnumerable<Task> tskList = unitOfWork.TaskRepository.Get().Where(m => timeInMonth.Contains(Convert.ToInt32(m.TimeMonth))).ToList();

                        IEnumerable<User_Task> usertskList = tskList.Select(x => new User_Task { UserId = x.UserId.HasValue ? x.UserId.Value : obj.Id, TaskId = x.TaskId, CompletionStatus = x.CompletionStatus.HasValue ? x.CompletionStatus.Value : false });

                        unitOfWork.UserTaskRepository.Insert(usertskList);
                        unitOfWork.Save();



                        return RedirectToAction("Index", "Dashboard", obj);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            return View();
        }

        // Sending confirm email to user
        private string SendActivationEmail(User obj)
        {
            Guid activationCode = Guid.NewGuid();

            UserActivation user = new UserActivation();
            user.ActivationId = obj.Id;
            user.AcitivationCode = activationCode;

            unitOfWork.UserActivationRepository.Insert(user);
            unitOfWork.Save();

            using (MailMessage mm = new MailMessage("latikasood.in@gmail.com", obj.Email))
            {
                mm.Subject = "Confirm your email";
                string body = "Hello " + obj.Name + ",";
                body += "<br /><br />One more click and you're ready to go.<br />We are sending you this email so you can confirm your registration.";
                body += "<br /><a href = '" + string.Format("{0}://{1}/Login/Activation/{2}", Request.Url.Scheme, Request.Url.Authority, activationCode) + "'>Confirm my email</a>";
                body += "<br /><br />If you're not signed up with Chicadresse and received this email by error, ignore it and our apologies for the inconvenience.<br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("latikasood.in@gmail.com", "akital1991");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                return "success";
            }
        }

        // On click of confirm email activating the account
        public ActionResult Activation()
        {
            if (RouteData.Values["id"] != null)
            {
                Guid activationCode = new Guid(RouteData.Values["id"].ToString());

                UserActivation userActivation = unitOfWork.UserActivationRepository.Get().Where(p => p.AcitivationCode == activationCode).FirstOrDefault();
                if (userActivation != null)
                {
                    unitOfWork.UserActivationRepository.Delete(userActivation);
                    unitOfWork.Save();
                }
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
                var obj = unitOfWork.UserRepository.Get().Where(a => a.Email.Equals(email)).FirstOrDefault();
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
                        NetworkCredential NetworkCred = new NetworkCredential("latikasood.in@gmail.com", "akital1991");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
            return View();
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

            User user = unitOfWork.UserRepository.Get().Where(a => a.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                user.Password = newPasswd;
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();

                return RedirectToAction("Index", "Dashboard", user);
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["userSession"] = null;
            System.Web.HttpContext.Current.Session["userEmail"] = null;
            return RedirectToAction("Login", "Login");
        }

    }
}