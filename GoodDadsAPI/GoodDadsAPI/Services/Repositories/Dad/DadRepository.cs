using GoodDadsAPI.Services.Common;
using GoodDadsAPI.Services.Schema;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace GoodDadsAPI.Services.Repositories
{
    public class DadRepository : IDadRepository
	{
		public string ConnectionString => GoodDadsResources.DatabaseConnectionString;

		public async Task<Dad> GetById(int id)
		{
			using (var conn = new SqlConnection(this.ConnectionString))
			{
				conn.Open();

                string query = "SELECT UserID, FirstName, LastName, Phone, Email, Employer, WorkPhone, MaritalStatusID FROM Dad WHERE UserID = @userid";

                var cmd = new SqlCommand(query, conn);

                SqlParameter parameter = new SqlParameter("@userid", SqlDbType.Int);
                parameter.Value = id;
                cmd.Parameters.Add(parameter);

                var reader = cmd.ExecuteReader();

                var schema = new Dad();
                while (reader.Read())
                {
                    schema.UserID = reader.GetInt32(0);
                    schema.FirstName = reader.GetString(1);
                    schema.LastName = reader.GetString(2);
                    schema.Phone = reader.GetString(3);
                    schema.Email = reader.GetString(4);
                    schema.Employer = reader.GetString(5);
                    schema.WorkPhone = reader.GetString(6);
                    schema.MaritalStatusID = reader.GetInt32(7);
                }

                reader.Close();
                conn.Close();

                return schema;
            }
		}

        public async Task<Dad> GetByEmail(string email)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                string query = "SELECT UserID, FirstName, LastName, Phone, Email, Employer, WorkPhone, MaritalStatusID FROM Dad WHERE Email = @Email";

                var cmd = new SqlCommand(query, conn);

                SqlParameter parameter = new SqlParameter("@Email", SqlDbType.VarChar);
                parameter.Value = email;
                cmd.Parameters.Add(parameter);

                var reader = cmd.ExecuteReader();

                var schema = new Dad();
                while (reader.Read())
                {
                    schema.UserID = reader.GetInt32(0);
                    schema.FirstName = reader.GetString(1);
                    schema.LastName = reader.GetString(2);
                    schema.Phone = reader.GetString(3);
                    schema.Email = reader.GetString(4);
                    schema.Employer = reader.GetString(5);
                    schema.WorkPhone = reader.GetString(6);
                    schema.MaritalStatusID = reader.GetInt32(7);
                }

                reader.Close();
                conn.Close();

                return schema;
            }
        }

        public async Task<int> Insert(Dad insertData)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder();

                query.Append("INSERT INTO Dad (FirstName, LastName, Phone, Email, Employer, WorkPhone, MaritalStatusID) ");
                query.Append("VALUES (@FirstName, @LastName, @Phone, @Email, @Employer, @WorkPhone, @MaritalStatusID);");

                var cmd = new SqlCommand(query.ToString(), conn);

                List<SqlParameter> columns = new List<SqlParameter>
                {
                    new SqlParameter("@FirstName", insertData.FirstName ?? string.Empty),
                    new SqlParameter("@LastName", insertData.LastName ?? string.Empty),
                    new SqlParameter("@Phone", insertData.Phone ?? string.Empty),
                    new SqlParameter("@Email", insertData.Email ?? string.Empty),
                    new SqlParameter("@Employer", insertData.Employer ?? string.Empty),
                    new SqlParameter("@WorkPhone", insertData.WorkPhone ?? string.Empty),
                    new SqlParameter("@MaritalStatusID", insertData.MaritalStatusID),
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

