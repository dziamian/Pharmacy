using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Models.Data_Transform_Objects
{
    public class GoogleAuthDTO
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
