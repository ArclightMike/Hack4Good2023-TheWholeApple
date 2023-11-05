using GoodDadsAPI.Services.Models;
using GoodDadsAPI.Services.Schema;

namespace GoodDadsAPI.Services.Repositories
{
    public interface ILoginRepository
    {
        Task<LoginSchema> GetByEmail(string email);

        Task<int> Insert(LoginSchema insertData);

        Task UpdatePassword(LoginSchema updateData);
    }
}
