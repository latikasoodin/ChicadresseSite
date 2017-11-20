using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.UnitOfWork;
using System.IO;
using BLL;

namespace ChicadresseSite.Controllers
{
    public class DashboardController : BaseController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Common common = new Common();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashbord Checklist
        public ActionResult Checklist()
        {
            var user = (User)ViewData["userSession"];
            var userId = user.Id;


            //To add by default first 29 tasks taskId to User_Task table according to marriage date month
            IEnumerable<User_Task> usertaskCompletelist = unitOfWork.UserTaskRepository.Get().Where(a=> a.UserId.Equals(userId));

            HashSet<int> favIds = new HashSet<int>(usertaskCompletelist.Select(s => s.TaskId));
            IEnumerable<Task> tskOfCurrentUser = unitOfWork.TaskRepository.Get().Where(m => favIds.Contains(m.TaskId)).ToList();

            ViewBag.TaskCount = usertaskCompletelist.Select(x => x.CompletionStatus.Equals(true)).Count();

            ViewBag.TotalTaskCompleted = usertaskCompletelist.Count();

            IEnumerable<Task_Timing> tskTiming = unitOfWork.TaskTimingRepository.Get();

            ViewBag.TotalMonths = usertaskCompletelist.Select(s => s.Task.TimeMonth).Distinct();

            ViewBag.TaskTiming = tskTiming;

            return View(tskOfCurrentUser);
        }

        //To get data of task
        [HttpGet]
        public JsonResult ChecklistGetTaskById(int taskId)
        {
            ChicadressEntities context = new ChicadressEntities();
            context.Configuration.ProxyCreationEnabled = false;
            Task tsk = context.Tasks.Find(taskId);
            return Json(tsk, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetTaskCompletionStatus(int taskId)
        {
            var user = (User)ViewData["userSession"];
            var userId = user.Id;

            IEnumerable<User_Task> usertask = unitOfWork.UserTaskRepository.Get().Where(a => a.UserId.Equals(userId) && a.TaskId.Equals(taskId));
            var utsk = usertask as User_Task;
            unitOfWork.UserTaskRepository.Update(utsk);
            unitOfWork.Save();

            IEnumerable<Task> task = unitOfWork.TaskRepository.Get().Where(a => a.UserId.Equals(userId) && a.TaskId.Equals(taskId));
            var tsk = task as Task;
            unitOfWork.TaskRepository.Update(tsk);
            unitOfWork.Save();

            return RedirectToAction("Checklist", "Dashboard");
        }

        // DELETE: /User/Delete
        public ActionResult DeleteTask(int? taskId)
        {
            unitOfWork.TaskRepository.Delete(taskId);
            unitOfWork.Save();
            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal save changes in checklist page for adding new task
        public ActionResult ChecklistAddRun(FormCollection form)
        {
            Task tsk = new Task();
            tsk.Title = Convert.ToString(form["InputEmail1"]);
            tsk.Description = Convert.ToString(form["comment"]);
            tsk.CompletionStatus = false;

            var user = (User)ViewData["userSession"];
            tsk.UserId = user.Id;
            //tsk.TimeMonth = 

            unitOfWork.TaskRepository.Insert(tsk);
            unitOfWork.Save();

            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal edit in checklist page
        [HttpPost]
        public ActionResult ChecklistEditRun(FormCollection form)
        {
            int id = Convert.ToInt32(form["taskId"]);
            Task tsk = unitOfWork.TaskRepository.Get().Where(a => a.TaskId.Equals(id)).FirstOrDefault();
            tsk.Title = Convert.ToString(form["Nombre"]);
            tsk.TimingId = Convert.ToInt32(form["Period"]);
            tsk.CategoryId = Convert.ToInt32(form["Categoria"]);
            tsk.Description = Convert.ToString(form["Notas"]);

            var user = (User)ViewData["userSession"];
            tsk.UserId = user.Id;

            unitOfWork.TaskRepository.Update(tsk);
            unitOfWork.Save();

            return RedirectToAction("Checklist", "Dashboard");
        }

        //Called on modal save changes in mywedding/index page
        public ActionResult WeddingRun(HttpPostedFileBase foto1, HttpPostedFileBase foto2)
        {
            var email = ViewData["userEmail"];
            User user = unitOfWork.UserRepository.Get().Where(a => a.Email.Equals(email)).FirstOrDefault();
            if (foto1 != null)
            {
                var path = common.testUpload(foto1);
                user.MyPic = path;
            }
            if (foto2 != null)
            {
                var path2 = common.testUpload(foto2);
                user.MyPartnerPic = path2;
            }

            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Dashbord Prestataires
        public ActionResult Prestataires()
        {
            var user = (User)ViewData["userSession"];
            var userId = user.Id;

            IEnumerable<User_FavouriteBusinessUser> favouriteList = unitOfWork.UserFavouriteBusinessRepository.Get().Where(a => a.UserId.Equals(userId));
            IEnumerable<Business_User> businessUsercompletelist = unitOfWork.BusinessUserRepository.Get();

            HashSet<int> favIds = new HashSet<int>(favouriteList.Select(s => s.BusinessUserId));
            IEnumerable<Business_User> supplierList = businessUsercompletelist.Where(m => favIds.Contains(m.BusinessUserId)).ToList();

            ViewBag.SupplierCount = supplierList.Count();
            return View(supplierList);
        }

    }
}