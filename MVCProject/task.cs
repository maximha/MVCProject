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
    
    public partial class task
    {
        public string workoutName { get; set; }
        public string taskName { get; set; }
        public string description { get; set; }
        public string time { get; set; }
        public string rev { get; set; }
    
        public virtual workout workout { get; set; }
    }
}
