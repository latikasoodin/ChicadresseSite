using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(15, ErrorMessage = "Password must contains minimun of 6 characters and maximum of 15 characters", MinimumLength = 6)]
        public string Password { get; set; }

        public string Country { get; set; }

        [Required(ErrorMessage = "Please Enter City")]
        public string StateCity { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Please Select Date")]
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
        public Nullable<bool> ActivationStatus { get; set; }

        //public HttpPostedFileBase File { get; set; }

        public virtual ICollection<Domain.Task> Tasks { get; set; }
        public virtual ICollection<User_Activation> User_Activation { get; set; }
        public virtual ICollection<User_FavouriteBusinessUser> User_FavouriteBusinessUser { get; set; }
        public virtual User_Role User_Role { get; set; }
        public virtual ICollection<User_Task> User_Task { get; set; }

        #endregion
    }
}
