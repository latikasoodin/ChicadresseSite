using Chicadresse.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.CustomViewEngine
{
    public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
    {
        public bool ShowCategories
        {
            get
            {
                return (bool)(ViewBag.ShowCategories ?? true);
            }
            set
            {
                ViewBag.ShowCategories = value;
            }
        }

        public UserViewModel UserSession
        {
            get
            {
                var userSession = HttpContext.Current.Session["userSession"];
                if (userSession == null)
                    return new UserViewModel();

                return (UserViewModel)userSession;
            }
        }
    }

    public abstract class BaseWebViewPage : BaseWebViewPage<dynamic>
    {

    }
}