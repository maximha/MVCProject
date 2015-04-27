using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVCProject.ApiModels
{
    public class TaskModel
    {
        public string taskName { get; set; }
        public string workoutName { get; set; }
        [JsonProperty("description")]
        public string descriptionTask { get; set; }
        [JsonProperty("time")]
        public string timeTask { get; set; }
        [JsonProperty("rev")]
        public string revTask { get; set; }
    }
}