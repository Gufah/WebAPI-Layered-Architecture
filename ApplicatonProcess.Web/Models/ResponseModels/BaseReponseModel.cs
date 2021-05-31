using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ApplicatonProcess.Web.Models
{
    public class BaseReponseModel<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false;
        [JsonProperty("errors")]
        public string[] Errors { get; set; } = { };
        [JsonProperty("data")]
        public T Data { get; set; }

        public class ResponseMessage
        {
            public string message { get; set; }
        }
    }
}
