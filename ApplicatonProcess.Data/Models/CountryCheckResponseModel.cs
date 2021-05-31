using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Data.Models
{
    public class CountryCheckResponseModel
    {
        public CountryResponse[] Country { get; set; }

        public class CountryResponse
        {
            public string name { get; set; }
        }
    }
}
