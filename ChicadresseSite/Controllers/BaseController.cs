using System.Linq;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base        
        public BaseController()
        {


        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["userSession"] = System.Web.HttpContext.Current.Session["userSession"];

            if (ViewData["userSession"] != null && ViewData.Values.Count() > 0)
            {
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
                filterContext.Result = new RedirectResult("~/Login/Index");

            }

            //check Session here
        }
    }
}