using ApplicatonProcess.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicatonProcess.Web.Models
{
    public class AssetSuccessResponseModel
    {
        [JsonProperty("asset_name")]
        public string AssetName { get; set; }

        [JsonProperty("department")]
        public Department Department { get; set; }

        [JsonProperty("country")]
        public string CountryOfDepartment { get; set; }

        [JsonProperty("email")]
        public string EMailAddressOfDepartment { get; set; }

        [JsonProperty("purchase_date")]
        public DateTime PurchaseDate { get; set; }
    }
}
