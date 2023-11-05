using GoodDadsAPI.Services.Common;
using GoodDadsAPI.Services.Schema;
using Microsoft.Data.SqlClient;
using System.Text;

namespace GoodDadsAPI.Services.Repositories
{
    public class DependentRepository : IDependentRepository
	{
		public string ConnectionString => GoodDadsResources.DatabaseConnectionString;

		public async Task<Dependent> GetById(int id)
		{
			using (var conn = new SqlConnection(this.ConnectionString))
			{
                return new Dependent();

            }
		}

        public async Task Insert(Dependent insertData)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder();

                query.Append("INSERT INTO Child(UserID, DateOfBirth, ContactStatusID, ChildSupportPerMonth) ");
                query.Append("VALUES(@UserID, @DateOfBirth, @ContactStatusID, @ChildSupportPerMonth);");

                var cmd = new SqlCommand(query.ToString(), conn);

                List<SqlParameter> columns = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", insertData.UserId),
                    new SqlParameter("@DateOfBirth", insertData.BirthDate),
                    new SqlParameter("@ContactStatusID", insertData.ContactStatusId),
                    new SqlParameter("@ChildSupportPerMonth", insertData.SupportPerMonth),
                };

                foreach (var column in columns) 
                {
                    cmd.Parameters.Add(column);
                }

                await cmd.ExecuteNonQueryAsync();

                conn.Close();

            }
        }
    }
}

