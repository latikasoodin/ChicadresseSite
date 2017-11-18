using DAL;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public abstract class BaseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Base        
        public BaseController()
        {


        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["userSession"] = System.Web.HttpContext.Current.Session["userSession"];
            ViewData["userEmail"] = System.Web.HttpContext.Current.Session["userEmail"];

            if (ViewData["userSession"] != null && ViewData.Values.Count() > 0)
            {
                User user = (User)ViewData["userSession"];
                DateTime dt = Convert.ToDateTime(user.MarriageDate);
                ViewBag.dt = Convert.ToString(dt);
                ViewBag.MarriageDate = user.MarriageDate;
                ViewBag.MarriageDateString = dt.ToString("dddd, dd MMMM yyyy");
                ViewBag.Name = Convert.ToString(user.Name);
                ViewBag.PartnerName = Convert.ToString(user.PartnerName);
                ViewBag.MyPic = user.MyPic;
                ViewBag.MyPartnerPic = user.MyPartnerPic;


                //var userID = (int)ViewData["userSession"];
                //ApplicationUser applicationUser = new ApplicationUser();
                //List<application_user> userList = applicationUser.getChildByUserID(userID);
                //ViewBag.commondata = userList;


                //// Get Role By User

                //string url = Request.Url.AbsoluteUri;
                //var rightExist = 0;
                //List<AppUserAggrigate> userRoleResult = applicationUser.GetRoleByuserId(userID);
                //foreach (var a in userRoleResult)
                //{
                //    if (url.Contains(a.MenuTitle))
                //    {
                //        rightExist = 1;
                //    }
                //}
                //if (rightExist == 0)
                //{
                //    // filterContext.Result = new RedirectResult("~/Login/Index");
                //}
                //ViewBag.userRoleResult = userRoleResult;
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/Login");

            }

            //check Session here
        }
    }
}