using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.UnitOfWork;

namespace ChicadresseSite.Controllers
{
    public class BusinessBaseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Base        
        public BusinessBaseController()
        {


        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["businessUserSession"] = System.Web.HttpContext.Current.Session["businessUserSession"];

            if (ViewData["businessUserSession"] != null && ViewData.Values.Count() > 0)
            {
               
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Business/Login");
            }

            //check Session here
        }
    }
}