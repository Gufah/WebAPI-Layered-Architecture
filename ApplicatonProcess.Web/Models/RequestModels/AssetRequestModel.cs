using FluentValidation;
using Newtonsoft.Json;
using ApplicatonProcess.Domain.Models;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace ApplicatonProcess.Web.Models
{
    [SwaggerSchema(Required = new[] { "asset_name", "department", "country", "department_email", "purchase_date" })]
    public class AssetRequestModel
    {
        [JsonProperty("asset_name")]
        public string AssetName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("department")]
        public Department Department { get; set; }

        [JsonProperty("country")]
        public string DepartmentCountry { get; set; }

        [JsonProperty("department_email")]
        public string DepartmentEmail{ get; set; }

        [JsonProperty("purchase_date")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("broken")]
        [DefaultValue(false)]
        public bool Broken { get; set; } = false;

    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-mm-dd";
        }
    }
}
