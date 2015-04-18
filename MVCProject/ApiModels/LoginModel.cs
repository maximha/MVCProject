using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ApiModels
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string publicKey { get; set; }
    }
}