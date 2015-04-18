using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.ApiModels
{
    public class TaskModel
    {
        public string taskName { get; set; }
        public string descriptionTask { get; set; }
        public string timeTask { get; set; }
        public string revTask { get; set; }
    }
}