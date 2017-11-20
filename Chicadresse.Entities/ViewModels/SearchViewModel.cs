using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class SearchViewModel
    {
        public int Page { get; set; }

        public int TotalRecords { get; set; }

        public string OrderBy { get; set; }
    }
}
