using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Chicadresse.Entities.ViewModels
{
    public class UserViewModel
    {
        #region ctor
        public UserViewModel()
        {

        }
        #endregion

        #region feilds/properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string StateCity { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? MarriageDate { get; set; }
        public int? User_Id { get; set; }
        public Nullable<int> ActivationId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerId { get; set; }
        public Nullable<int> Budget { get; set; }
        public Nullable<int> NoOfGuest { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public byte[] MyPic { get; set; }
        public byte[] MyPartnerPic { get; set; }

        public HttpPostedFileBase File { get; set; }

        #endregion
    }
}
