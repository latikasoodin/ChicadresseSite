using AutoMapper;
using Chicadresse.Business.Services;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region ctor

        public LoginController(IUserService userService)
        {
            this._userService = userService;
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
        public ActionResult Login(LoginViewModel objUser)
        {
            if (objUser != null)
            {
                var obj = this._userService.GetUser(objUser.Email, objUser.Password);
                if (obj != null)
                {
                    var user = Mapper.Map<User, UserViewModel>(obj);
                    System.Web.HttpContext.Current.Session["userSession"] = user;
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


        //Logout
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["userSession"] = null;
            return RedirectToAction("Login", "Login");
        }

        #endregion
    }
}