using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.UnitOfWork;
using DAL;

namespace ChicadresseSite.Controllers
{
    public class BusinessController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        // Login business
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Business_User objUser)
        {
            if (objUser != null)
            {
                var obj = unitOfWork.BusinessUserRepository.Get().Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    System.Web.HttpContext.Current.Session["businessUserSession"] = obj;
                    return RedirectToAction("MonEspacePro", "MonCompte", obj);
                }
            }
            return View(objUser);
        }

        // Signup business
        public ActionResult SignupStep1()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult SignupStep1(Business_User obj, FormCollection form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ChicadresseEntities db = new ChicadresseEntities();

        //        obj.Username = form["Username"].ToString();
        //        obj.Password = form["Password"].ToString();
        //        obj.ContactPerson = form["ContactPerson"].ToString();
        //        obj.Email = form["Email"].ToString();
        //        obj.Telephone = Convert.ToInt32(form["Telephone"]);
        //        obj.Mobile = Convert.ToInt32(form["TelefonoMovil"]);
        //        obj.Fax = Convert.ToInt32(form["Fax"]);
        //        obj.Webpage = form["Web"].ToString();
        //        obj.Country = form["Cntry"].ToString();
        //        obj.City = form["txtStrPoblacion"].ToString();
        //        obj.Postcode = form["Postcode"].ToString();
        //        obj.Address = form["Address"].ToString();
        //        obj.BusinessName = form["BusinessName"].ToString();

        //        db.Business_User.Add(obj);
        //        db.SaveChanges();
        //        return RedirectToAction("SignupStep2");
        //    }
        //    return View();
        //}

        //// Signup business
        //public ActionResult SignupStep2()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SignupStep2(FormCollection fc, HttpPostedFileBase file)
        //{
        //    //    Business_User tbl = new Business_User();
        //    //    var allowedExtensions = new[] {
        //    //    ".Jpg", ".png", ".jpg", "jpeg"
        //    //};
        //    //    tbl.Id = fc["Id"].ToString();
        //    //    tbl.Image_url = file.ToString(); //getting complete url  
        //    //    tbl.Name = fc["Name"].ToString();
        //    //    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
        //    //    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
        //    //    if (allowedExtensions.Contains(ext)) //check what type of extension  
        //    //    {
        //    //        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
        //    //        string myfile = name + "_" + tbl.Id + ext; //appending the name with id  
        //    //                                                   // store the file inside ~/project folder(Img)  
        //    //        var path = Path.Combine(Server.MapPath("~/Img"), myfile);
        //    //        tbl.Image_url = path;
        //    //        obj.tbl_details.Add(tbl);
        //    //        obj.SaveChanges();
        //    //        file.SaveAs(path);
        //    //    }
        //    //    else
        //    //    {
        //    //        ViewBag.message = "Please choose only Image file";
        //    //    }
        //    return RedirectToAction("SignupStep3");
        //}

        //// Signup business
        //public ActionResult SignupStep3()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SignupStep3(Business_User obj, FormCollection form)
        //{
        //    return RedirectToAction("SignupStep4");
        //}

        //// Signup business
        //public ActionResult SignupStep4()
        //{
        //    return View();
        //}


        

    }
}