using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ApiModels
{
    public class RegistrationModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}