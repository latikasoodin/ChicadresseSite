using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class GuestViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int AttendanceId { get; set; }

        [Required]
        [Display(Name = "Group")]
        public int? GroupId { get; set; }

        [Required]
        [Display(Name = "Table")]
        public int? TableId { get; set; }

        public string Name => String.Concat(FirstName ?? string.Empty, " " + LastName ?? string.Empty);

        public List<CompanionViewModel> Companions { get; set; }

    }
}
