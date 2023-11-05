using GoodDadsAPI.Services.Schema;

namespace GoodDadsAPI.Services.Repositories
{
    public interface IAddressRepository
    {
        Task<int> Insert(Address insertData);
    }
}
