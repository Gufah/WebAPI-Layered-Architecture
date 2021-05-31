using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public Department Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAddress { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
