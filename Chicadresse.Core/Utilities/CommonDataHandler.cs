using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chicadresse.Core.Utilities
{
    public class CommonDataHandler : IDataHandler
    {
        #region feilds
        #endregion

        #region ctor
        public CommonDataHandler()
        {

        }
        #endregion

        #region methods
        
        public string TestUpload(HttpPostedFileBase file)
        {
            string myfile = ""; string path = ""; string dbpath = "";
            if (file != null)
            {
                var allowedExtensions = new[] {
                        ".Jpg", ".png", ".jpg", "jpeg",".xls",".xls",".xlsx",".xlsm",".docx",".doc",".pdf",".gif"
                    };

                var application_Directory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "Content/assets";
                // var application_Directory = System.Web.Hosting.HostingEnvironment.MapPath(HttpContext.Request.ApplicationPath);//ConfigurationManager.AppSettings["LogDirectory"].ToString();
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-abc.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    // Year and month Folder Created if not exist
                    DateTime dt = DateTime.Now;
                    application_Directory = application_Directory + "/" + dt.Year + "/" + dt.Month;
                    dbpath = "Content/assets/" + dt.Year + "/" + dt.Month + "/";
                    if (!Directory.Exists(application_Directory))
                    {
                        Directory.CreateDirectory(application_Directory);

                    }
                    // End Folder creation and cheking of the folder 

                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension 
                    var datestring = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                    myfile = datestring + "_" + name + "_" + ext; //appending the name with id
                    path = Path.Combine(application_Directory, myfile);
                    dbpath = dbpath + myfile;
                    file.SaveAs(path);

                    return dbpath;
                }

                // Stroe myfile in Database as a path;
            }
            return path;
        }

        #endregion
    }
}
