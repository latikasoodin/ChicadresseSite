//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chicadresse.Entities.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Guest_Table
    {
        public int Id { get; set; }
        public Nullable<int> GuestId { get; set; }
        public Nullable<int> TableId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Guest_Details Guest_Details { get; set; }
        public virtual Table Table { get; set; }
    }
}
