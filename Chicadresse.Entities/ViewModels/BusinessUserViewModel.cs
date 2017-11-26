using Chicadresse.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class BusinessUserViewModel
    {
        #region ctor
        public BusinessUserViewModel()
        {

        }
        #endregion

        #region feilds/properties

        public int BusinessUserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public Nullable<int> Telephone { get; set; }
        public Nullable<int> Mobile { get; set; }
        public Nullable<int> Fax { get; set; }
        public string Webpage { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public Nullable<int> CategoryId { get; set; }

        public virtual Tasks_Category Tasks_Category { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Response_Template> Response_Template { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<User_FavouriteBusinessUser> User_FavouriteBusinessUser { get; set; }
        public virtual ICollection<Domain.Task> Tasks { get; set; }

        #endregion
    }
}
