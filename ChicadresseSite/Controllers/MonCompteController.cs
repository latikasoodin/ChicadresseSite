using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class MonCompteController : BusinessBaseController
    {
        // GET: MyAccount
        public ActionResult MonCompte()
        {
            return View();
        }

        // GET: Details
        public ActionResult MonEspacePro()
        {
            return View();
        }
    }
}