using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ApplicatonProcess.Domain.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Department
    {
        HQ,
        Store1,
        Store2,
        Store3,
        MaintenanceStation
    }
}
