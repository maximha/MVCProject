//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class workout
    {
        public workout()
        {
            this.tasks = new HashSet<task>();
        }
    
        public string workoutName { get; set; }
        public string userName { get; set; }
    
        public virtual ICollection<task> tasks { get; set; }
        public virtual user user { get; set; }
    }
}