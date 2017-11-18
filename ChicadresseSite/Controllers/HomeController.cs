using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(HttpPostedFileBase fileUpload)
        {
            // new code 
            var applicationDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string fileName = Path.Combine(Server.MapPath("~/Content/ar"), Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName));
            fileUpload.SaveAs(fileName);

            string conString = "";
            string ext = Path.GetExtension(fileUpload.FileName);
            if (ext.ToLower() == ".xls")
            {
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
            }
            else if (ext.ToLower() == ".xlsx")
            {
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            // End 
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult Contact(HttpPostedFileBase fileUpload)
        //{



        //    var applicationDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
        //    string fileName = Path.Combine(Server.MapPath("~/Content/ar"), Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName));
        //    fileUpload.SaveAs(fileName);

        //    string conString = "";
        //    string ext = Path.GetExtension(fileUpload.FileName);
        //    if (ext.ToLower() == ".xls")
        //    {
        //        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
        //    }
        //    else if (ext.ToLower() == ".xlsx")
        //    {
        //        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //    }

        //    string query = "Select [EligibilityDate],[SlipNo],[PatientName],[InsuranceID],[GroupID],[DoctorID],[EffectiveDate],[TerminationDate],[Status],[EligibilitySource],[EligibilityComment],[uniqueID] from [Eligibility$]";
        //    OleDbConnection con = new OleDbConnection(conString);
        //    if (con.State == System.Data.ConnectionState.Closed)
        //    {
        //        con.Open();
        //    }
        //    OleDbCommand cmd = new OleDbCommand(query, con);
        //    OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
        //    //yuvrajrandive87@gmail.com
        //    da.Dispose();
        //    con.Close();
        //    con.Dispose();

        //    Workflow_DLL.Eligibility elgibilityObj = new Workflow_DLL.Eligibility();
        //    // Workflow_DLL.ArMasterCallHistory arCallHistory = new Workflow_DLL.ArMasterCallHistory();

        //    // Import to Database
        //    using (workflow_managementEntities6 dc = new workflow_managementEntities6())
        //    {
        //        int ArMasterId;

        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            var uniqueId = dr["PatientName"].ToString() + dr["InsuranceID"].ToString() + dr["GroupID"].ToString() + dr["SlipNo"].ToString() + dr["EligibilityDate"].ToString() + dr["uniqueID"].ToString();

        //            if (uniqueId.Length >= 6)
        //            {
        //                var eligibilityUpdate = dc.Eligibilities.Where(a => a.uniqueID.Equals(uniqueId)).FirstOrDefault();

        //                // Insert
        //                elgibilityObj.uniqueID = uniqueId;
        //                string EligibilityDate = dr["EligibilityDate"].ToString();
        //                DateTime dateTime2;
        //                if (DateTime.TryParse(EligibilityDate, out dateTime2))
        //                {
        //                    elgibilityObj.EligibilityDate = dateTime2;
        //                }
        //                else
        //                {
        //                    elgibilityObj.EligibilityDate = null; // <-- Control flow goes here
        //                }
        //                elgibilityObj.SlipNo = dr["SlipNo"].ToString();
        //                elgibilityObj.PatientName = dr["PatientName"].ToString();
        //                elgibilityObj.InsuranceID = Convert.ToInt32(dr["InsuranceID"].ToString());
        //                elgibilityObj.GroupID = GroupID;
        //                elgibilityObj.DoctorID = Convert.ToInt32(dr["DoctorID"].ToString());


        //                string EffectiveDate = dr["EffectiveDate"].ToString();
        //                DateTime EffectiveD;
        //                if (DateTime.TryParse(EffectiveDate, out EffectiveD))
        //                {
        //                    elgibilityObj.EffectiveDate = EffectiveD;
        //                }
        //                else
        //                {
        //                    elgibilityObj.EffectiveDate = null; // <-- Control flow goes here
        //                }

        //                string TerminationDate = dr["TerminationDate"].ToString();
        //                DateTime TerminationD;
        //                if (DateTime.TryParse(TerminationDate, out TerminationD))
        //                {
        //                    elgibilityObj.TerminationDate = TerminationD;
        //                }
        //                else
        //                {
        //                    elgibilityObj.TerminationDate = null; // <-- Control flow goes here
        //                }

        //                elgibilityObj.Status = dr["Status"].ToString();
        //                elgibilityObj.EligibilitySource = dr["EligibilitySource"].ToString();
        //                elgibilityObj.EligibilityComment = dr["EligibilityComment"].ToString();
        //                dc.Eligibilities.Add(elgibilityObj);
        //                dc.SaveChanges();

        //            }
        //        }
        //    }
        //    // new code 
        //    var applicationDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
        //    string fileName = Path.Combine(Server.MapPath("~/Content/ar"), Guid.NewGuid().ToString() + Path.GetExtension(fileUpload.FileName));
        //    fileUpload.SaveAs(fileName);

        //    string conString = "";
        //    string ext = Path.GetExtension(fileUpload.FileName);
        //    if (ext.ToLower() == ".xls")
        //    {
        //        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; ;
        //    }
        //    else if (ext.ToLower() == ".xlsx")
        //    {
        //        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //    }
        //    // End 
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}



    }
}