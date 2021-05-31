using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Repsitories
{
    public interface IHttpClient
    {
        Task<string> GetCountryByName(string countryName);
    }
}
