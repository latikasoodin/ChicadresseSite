using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class FilterViewModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; } = 20;

        public string OrderBy { get; set; }
    }
}
