using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicatonProcess.Web.Services
{
    public interface IAssetDetailService
    {
        Task<int> SaveAssetDetails(Asset asset);

        Task<Tuple<int>> UpdateAssetDetails(int id, Asset asset);

        Task<IEnumerable<AssetSuccessResponseModel>> GetAssetDetails();

        Task<AssetSuccessResponseModel> GetAssetDetailsById(int Id);
        Task<Tuple<int>> DeleteAssetDetailsById(int id);
    }
}
