using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Country { get; set; }
        public string StateCity { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<System.DateTime> MarriageDate { get; set; }
        public Nullable<int> User_Id { get; set; }
        public Nullable<int> ActivationId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerId { get; set; }
        public Nullable<int> Budget { get; set; }
        public Nullable<int> NoOfGuest { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string MyPic { get; set; }
        public string MyPartnerPic { get; set; }
    }
}
