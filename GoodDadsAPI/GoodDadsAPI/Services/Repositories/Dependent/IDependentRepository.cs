using GoodDadsAPI.Services.Schema;

namespace GoodDadsAPI.Services.Repositories
{
    public interface IDependentRepository
    {
        Task<Dependent> GetById(int id);

        Task Insert(Dependent insertData);
    }
}
