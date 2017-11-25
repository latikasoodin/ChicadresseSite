using AutoMapper;
using Chicadresse.Business.Services.Guests;
using Chicadresse.Entities.Domain;
using Chicadresse.Entities.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ChicadresseSite.Controllers
{
    public class InvitesController : Controller
    {

        #region Fields

        private readonly IGuestService _guestService;

        #endregion


        #region ctor

        public InvitesController(IGuestService guestService)
        {
            this._guestService = guestService;
        }

        #endregion

        #region Actions

        // GET: Guest
        public ActionResult Index()
        {
            int weddingId = 1;
            GuestFilterViewModel filter = new GuestFilterViewModel
            {
                WeddingId = weddingId,   //TODO: get wedding details from claims
                PageNumber = 0,
                PageSize = 20
            };

            ViewBag.InviteStats = _guestService.GetGuestStats(weddingId);
            var data = _guestService.GetGuestList(filter);
            return View(data);
        }

        public ActionResult Add()
        {
            return PartialView("_AddGuest", new GuestViewModel());
        }

        public ActionResult Edit(int id)
        {
            Guest_Details guest = this._guestService.GetGuest(id);
            var model = Mapper.Map<GuestViewModel>(guest);
            return PartialView("_AddGuest", model);
        }

        [HttpPost]
        public ActionResult UpdatePresence(int presenceId, int guestId)
        {
            try
            {
                this._guestService.UpdatePresence(presenceId, guestId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateTable(int tableId, int guestId)
        {
            try
            {
                this._guestService.UpdateTable(tableId, guestId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UpdateGroup(int groupId, int guestId)
        {
            try
            {
                this._guestService.UpdateGroup(groupId, guestId);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Download()
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("Guest List " + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            doc.SetMargins(1f, 0.5f, 1f, 0.5f);

            //Create PDF Table with 4 columns  
            PdfPTable tableLayout = new PdfPTable(5);
            doc.SetMargins(0f, 0f, 0f, 0f);
            //Create PDF Table  

            //file will created in this path  
            string strAttachment = Server.MapPath("~/Downloadss/" + strPDFFileName);

            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(PdfContent(tableLayout, "Guest List"));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return File(workStream, "application/pdf", strPDFFileName);
        }

        [HttpPost]
        public ActionResult Save(GuestViewModel guest)
        {
            if (ModelState.IsValid)
            {
                Guest_Details data = Mapper.Map<Guest_Details>(guest);
                data.Guest_Table = new List<Guest_Table>();
                data.Guest_Table.Add(new Guest_Table
                {
                    TableId = guest.TableId,
                    CreatedBy = 1, //login user
                    CreatedDate = DateTime.Now,
                    ModifiedBy = 1,
                    IsDeleted = false,
                    ModifiedDate = DateTime.Now
                });

                data.WeddingId = 1; // login user wedding
                data.CreatedBy = 1; //login user
                data.ModifiedBy = 1;
                data.IsDeleted = false;

                foreach (Guest_Companinons companion in data.Guest_Companinons)
                {
                    companion.GuestId = data.Id;
                    companion.CreatedBy = 1;
                    companion.ModifiedBy = 1;
                }
                _guestService.Save(data);
                return PartialView("_GuestList", this._guestService.GetGuestList(new GuestFilterViewModel
                {
                    WeddingId = 1
                }));
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { Validation = ModelState.Values }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View(guest);
            }
        }


        public ActionResult DeleteModal(int id)
        {
            Guest_Details guest = this._guestService.GetGuest(id);
            var model = Mapper.Map<GuestViewModel>(guest);
            return PartialView("_DeleteGuest", model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            this._guestService.Delete(id);
            return PartialView("_GuestList", this._guestService.GetGuestList(new GuestFilterViewModel
            {
                WeddingId = 1
            }));
        }
        #endregion

        #region Private 

        public PdfPTable PdfContent(PdfPTable tableLayout, string title)
        {
            float[] headers = { 50, 24, 45, 35, 50 };
            //Header Widths  
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            //Add Title to the PDF file at the top  

            tableLayout.AddCell(new PdfPCell(new Phrase(title, new Font(Font.FontFamily.HELVETICA, 8, 1, new iTextSharp.text.BaseColor(0, 0, 0))))
            {
                Colspan = 12,
                Border = 0,
                PaddingBottom = 5,
                HorizontalAlignment = Element.ALIGN_CENTER
            });


            ////Add header  
            PDFGenerator.AddCellToHeader(tableLayout, "Guest Id");
            PDFGenerator.AddCellToHeader(tableLayout, "Name");
            PDFGenerator.AddCellToHeader(tableLayout, "Presence");
            PDFGenerator.AddCellToHeader(tableLayout, "Group");
            PDFGenerator.AddCellToHeader(tableLayout, "Table");

            ////Add body 
            int weddingId = 1; // should be based on login

            IEnumerable<Guest_Details> details = this._guestService.GuestList(weddingId);
            foreach (Guest_Details guest in details)
            {

                PDFGenerator.AddCellToBody(tableLayout, guest.Id.ToString());
                PDFGenerator.AddCellToBody(tableLayout, guest.FirstName + " " + guest.LastName);
                PDFGenerator.AddCellToBody(tableLayout, guest.Attendance.Name);
                PDFGenerator.AddCellToBody(tableLayout, guest.Group.Name);
                var table = guest.Guest_Table.FirstOrDefault();
                if (table != null)
                {
                    PDFGenerator.AddCellToBody(tableLayout, table.Table.TableName);
                }
                else
                {
                    PDFGenerator.AddCellToBody(tableLayout, string.Empty);
                }

            }

            return tableLayout;
        }

        #endregion

    }
}