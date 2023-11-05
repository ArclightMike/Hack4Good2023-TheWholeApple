using GoodDadsAPI.Services.Common;
using GoodDadsAPI.Services.Schema;
using Microsoft.Data.SqlClient;
using System.Text;

namespace GoodDadsAPI.Services.Repositories
{
    public class AddressRepository : IAddressRepository
	{
		public string ConnectionString => GoodDadsResources.DatabaseConnectionString;

        public async Task<int> Insert(Address insertData)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder();

                query.Append("INSERT INTO Address (UserID, LineOne, LineTwo, City, State, Zip) ");
                query.Append("VALUES (@UserID, @LineOne, @LineTwo, @City, @State, @Zip);");

                var cmd = new SqlCommand(query.ToString(), conn);

                List<SqlParameter> columns = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", insertData.UserID),
                    new SqlParameter("@LineOne", insertData.LineOne ?? string.Empty),
                    new SqlParameter("@LineTwo", insertData.LineTwo ?? string.Empty),
                    new SqlParameter("@City", insertData.City ?? string.Empty),
                    new SqlParameter("@State", insertData.State ?? string.Empty),
                    new SqlParameter("@Zip", insertData.Zip ?? string.Empty),
                };

                foreach (var column in columns) 
                {
                    cmd.Parameters.Add(column);
                }

                int insertedPrimaryKey = Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();

                return insertedPrimaryKey;
            }
        }
    }
}

