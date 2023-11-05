using GoodDadsAPI.Services.Schema;

namespace GoodDadsAPI.Services.Repositories
{
    public interface IMaritalStatusRepository
    {
        Task<MaritalStatus> GetById(int id);
    }
}
