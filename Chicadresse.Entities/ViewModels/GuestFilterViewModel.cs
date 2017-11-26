using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class GuestFilterViewModel : FilterViewModel
    {
        public int WeddingId { get; set; }

        public string Name { get; set; }
    }
}
