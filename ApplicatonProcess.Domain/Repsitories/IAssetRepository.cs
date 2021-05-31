using ApplicatonProcess.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Repositories
{
    // this interface describes operations that can be performed against the Db
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> Get();
        Task<Asset> Get(int id);
        Task<int> Create(Asset asset);
        Task<int> Put(Asset asset);
        Task<int> Delete(int id);
    }
}
