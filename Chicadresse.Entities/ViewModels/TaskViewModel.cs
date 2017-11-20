using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chicadresse.Entities.ViewModels
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> TimingId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> UserId { get; set; }

        //public virtual Task_Timing Task_Timing { get; set; }
        //public virtual Tasks_Category Tasks_Category { get; set; }
        //public virtual User User { get; set; }
    }
}
