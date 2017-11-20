using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chicadresse.Business.Services;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;

namespace ChicadresseSite.Controllers
{
    public class DashboardController : Controller
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion

        #region ctor

        public DashboardController(IUserService userService)
        {
            this._userService = userService;
        }

        #endregion
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        #region Actions



        #endregion
    }
}