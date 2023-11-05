using GoodDadsAPI.Services.Common;
using GoodDadsAPI.Services.Schema;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GoodDadsAPI.Services.Repositories
{
    public class MaritalStatusRepository : IMaritalStatusRepository
	{
		public string ConnectionString => GoodDadsResources.DatabaseConnectionString;

		public async Task<MaritalStatus> GetById(int id)
		{
			using (var conn = new SqlConnection(this.ConnectionString))
			{
				conn.Open();

                string query = "SELECT MaritalStatusID, MaritalStatus FROM MaritalStatus; WHERE MaritalStatusID = @id";

                var cmd = new SqlCommand(query, conn);

                SqlParameter parameter = new SqlParameter("@id", SqlDbType.Int);
                parameter.Value = id;
                cmd.Parameters.Add(parameter);

                var reader = cmd.ExecuteReader();

                var schema = new MaritalStatus();
                while (reader.Read())
                {
                    schema.MaritalStatusID = reader.GetInt32(0);
                    schema.MaritalStatusValue = reader.GetString(1);
                }

                reader.Close();
                conn.Close();

                return schema;
            }
		}
	}
}

