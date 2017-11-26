using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class InviteStats
    {
        public int Total { get; set; }

        public int Confirms { get; set; }

        public int Waiting { get; set; }

        public int Tables { get; set; }

    }
}
