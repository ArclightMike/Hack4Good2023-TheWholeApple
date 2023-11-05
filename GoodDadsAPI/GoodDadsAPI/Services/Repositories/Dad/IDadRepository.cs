using GoodDadsAPI.Services.Schema;

namespace GoodDadsAPI.Services.Repositories
{
    public interface IDadRepository
    {
        Task<Dad> GetById(int id);

        Task<Dad> GetByEmail(string email);

        Task<int> Insert(Dad insertData);
    }
}
