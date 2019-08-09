using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sisuni.Models {
    public partial class AccesToken {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}