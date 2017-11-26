using AutoMapper;
using Chicadresse.Business.Services;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using Chicadresse.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class DashboardController : BaseController
    {
        //private Common common = new Common();

        #region Fields

        private readonly IUserService _userService;
        private readonly IUserTaskService _userTaskService;
        private readonly ITaskService _taskService;
        private readonly ITaskTimingService _taskTimingService;
        private readonly IBusinessUserService _businessUserService;
        private readonly IUserFavouriteBusinessService _userFavouriteBusinessService;

        CommonDataHandler cdh = new CommonDataHandler();

        #endregion

        #region ctor

        public DashboardController(IUserService userService, IUserTaskService userTaskService, ITaskService taskService, ITaskTimingService taskTimingService, IBusinessUserService businessUserService, IUserFavouriteBusinessService userFavouriteBusinessService)
        {
            this._userService = userService;
            this._userTaskService = userTaskService;
            this._taskService = taskService;
            this._taskTimingService = taskTimingService;
            this._businessUserService = businessUserService;
            this._userFavouriteBusinessService = userFavouriteBusinessService;
        }

        #endregion

        #region Actions

        // GET: Dashboard
        public ActionResult Index()
        {
            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var userId = user.Id;

            IEnumerable<User_FavouriteBusinessUser> favouriteList = _userFavouriteBusinessService.GetByUserId(userId);
            IEnumerable<Business_User> businessUsercompletelist = _businessUserService.Get();

            HashSet<int> favIds = new HashSet<int>(favouriteList.Select(s => s.BusinessUserId));
            IEnumerable<Business_User> supplierList = businessUsercompletelist.Where(m => favIds.Contains(m.BusinessUserId)).ToList();

            IEnumerable<BusinessUserViewModel> supplierModel = Mapper.Map<IEnumerable<Business_User>, IEnumerable<BusinessUserViewModel>>(supplierList);
            ViewBag.SupplierCount = supplierModel.Count();
            ViewBag.SupplierModel = supplierModel;
            return View();
        }

        // GET: Dashbord Checklist
        public ActionResult Checklist()
        {
            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var userId = user.Id;

            //To add by default tasks by timeMonth taskId to User_Task table according to marriage date month
            IEnumerable<User_Task> usertaskCompletelist = _userTaskService.GetById(userId);

            HashSet<int> favIds = new HashSet<int>(usertaskCompletelist.Select(s => s.TaskId));
            IEnumerable<Task> tskOfCurrentUser = _taskService.GetByTaskId(favIds);
            IEnumerable<TaskViewModel> tskList = Mapper.Map<IEnumerable<Task>, IEnumerable<TaskViewModel>>(tskOfCurrentUser);

            ViewBag.TaskCount = usertaskCompletelist.Select(x => x.CompletionStatus.Equals(true)).Count();
            ViewBag.TotalTaskCompleted = usertaskCompletelist.Count();
            IEnumerable<Task_Timing> tskTiming = _taskTimingService.GetTask();
            ViewBag.TotalMonths = usertaskCompletelist.Select(s => s.Task.TimeMonth).Distinct();
            ViewBag.TaskTiming = tskTiming;

            //var abc = usertaskCompletelist.Select(s => s.Task.TimeMonth).Distinct();

            //foreach (int i in ViewBag.TotalMonths)
            //{
            //    IEnumerable<Task_Timing>
            //    tskOfCurrentUser.Select(x => abc.Contains(x.TimeMonth)
            //}
            
            //    _taskRepository.Get().Where(m => timeInMonth.Contains(Convert.ToInt32(m.TimeMonth))).ToList();


            //ViewBag.TotalTaskCountAccPerMonth = tskOfCurrentUser.Select(x => x.TimeMonth).Distinct().ToList().Count();
            //ViewBag.CompletedTaskAccPerMonth = tskOfCurrentUser.Select(x => x.CompletionStatus).Count();

            return View(tskList);
        }

        //To get data of task
        [HttpGet]
        public JsonResult ChecklistGetTaskById(int taskId)
        {
            Task task = _taskService.GetByTaskId(taskId);
            var result = Json(new { TaskId = task.TaskId, Title = task.Title, Description = task.Description }, JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult SetTaskCompletionStatus(int taskId, bool checkStatus)
        {
            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var userId = user.Id;

            User_Task usertask = _userTaskService.GetByUserIdTaskId(userId, taskId);
            usertask.CompletionStatus = checkStatus;
            _userTaskService.Update(usertask);

            Task task = _taskService.GetByUserIdTaskId(userId, taskId);
            task.CompletionStatus = checkStatus;
            _taskService.UpdateTask(task);

            return RedirectToAction("Checklist", "Dashboard");
        }

        // DELETE: /User/Delete
        public ActionResult DeleteTask(int taskId)
        {
            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var userId = user.Id;

            _taskService.DeleteById(taskId);
            _userTaskService.DeleteByUserIdandTaskId(userId, taskId);
            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal save changes in checklist page for adding new task
        public ActionResult ChecklistAddRun(FormCollection form)
        {
            Task tsk = new Task();
            tsk.Title = Convert.ToString(form["InputEmail1"]);
            tsk.Description = Convert.ToString(form["comment"]);
            tsk.TimeMonth = Convert.ToInt32(form["timemonthid"]);
            tsk.CompletionStatus = false;
            int timemonth = Convert.ToInt32(tsk.TimeMonth);

            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            tsk.UserId = user.Id;
            if (timemonth > -1)
            {
                Task_Timing data = _taskTimingService.GetByTimeMonth(tsk.TimeMonth);
                tsk.TimingId = data.TimingId;
            }
            Task newTask = _taskService.AddTask(tsk);

            User_Task usrTsk = new User_Task()
            {
                UserId = user.Id,
                TaskId = newTask.TaskId,
                CompletionStatus = false
            };
            _userTaskService.Insert(usrTsk);

            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal edit in checklist page
        [HttpPost]
        public ActionResult ChecklistEditRun(FormCollection form)
        {
            int id = Convert.ToInt32(form["taskId"]);
            Task tsk = _taskService.GetByTaskId(id);
            tsk.Title = Convert.ToString(form["InputEmail1"]);
            tsk.Description = Convert.ToString(form["comment"]);

            _taskService.UpdateTask(tsk);
            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal save changes in mywedding/index page
        public ActionResult WeddingRun(HttpPostedFileBase foto1, HttpPostedFileBase foto2)
        {
            UserViewModel usermodel = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var email = usermodel.Email;

            User user = _userService.GetUserByEmail(email);
            if (foto1 != null)
            {
                var path = cdh.TestUpload(foto1);
                user.MyPic = path;
            }
            if (foto2 != null)
            {
                var path2 = cdh.TestUpload(foto2);
                user.MyPartnerPic = path2;
            }

            _userService.UpdateUser(user);
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Dashbord Prestataires
        public ActionResult Prestataires()
        {
            UserViewModel user = (UserViewModel)System.Web.HttpContext.Current.Session["userSession"];
            var userId = user.Id;

            IEnumerable<User_FavouriteBusinessUser> favouriteList = _userFavouriteBusinessService.GetByUserId(userId);
            IEnumerable<Business_User> businessUsercompletelist = _businessUserService.Get();

            HashSet<int> favIds = new HashSet<int>(favouriteList.Select(s => s.BusinessUserId));
            IEnumerable<Business_User> supplierList = businessUsercompletelist.Where(m => favIds.Contains(m.BusinessUserId)).ToList();

            IEnumerable<BusinessUserViewModel> supplierModel = Mapper.Map<IEnumerable<Business_User>, IEnumerable<BusinessUserViewModel>>(supplierList);
            ViewBag.SupplierCount = supplierModel.Count();
            return View(supplierModel);
        }

        #endregion

    }
}