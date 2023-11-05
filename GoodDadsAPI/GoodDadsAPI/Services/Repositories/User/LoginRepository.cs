using GoodDadsAPI.Services.Common;
using GoodDadsAPI.Services.Schema;
using Microsoft.Data.SqlClient;
using System.Text;

namespace GoodDadsAPI.Services.Repositories
{
    public class LoginRepository : ILoginRepository
	{
		public string ConnectionString => GoodDadsResources.DatabaseConnectionString;

		public async Task<LoginSchema> GetByEmail(string email)
		{
			using (var conn = new SqlConnection(this.ConnectionString))
			{
                conn.Open();

                StringBuilder query = new StringBuilder();

                //Top 1 needs fixed for multiple records with the same email.
                query.Append("SELECT TOP 1 UserID FROM Dad WHERE Email = @Email");

                var cmd = new SqlCommand(query.ToString(), conn);

                cmd.Parameters.Add(new SqlParameter("@Email", email));

                var reader = cmd.ExecuteReader();

                int? dadUserId = null;
                while (reader.Read())
                {
                    dadUserId = reader.GetInt32(0);
                }

                reader.Close();

                if (dadUserId == null)
                {
                    return null;
                }

                query.Clear();
                //ToDo: Refactor the above to get the user through dadrepository.

                query.Append("SELECT LoginID, UserID, AdminID, password FROM Login WHERE UserID = @UserID;");
                cmd = new SqlCommand(query.ToString(), conn);

                cmd.Parameters.Add(new SqlParameter("@UserID", dadUserId));

                var reader2 = cmd.ExecuteReader();

                var login = new LoginSchema();
                while (reader2.Read())
                {
                    login.LoginID = reader2.GetInt32(0);
                    login.UserID = reader2.IsDBNull(1) ? null : reader2.GetInt32(1);
                    login.AdminID = reader2.IsDBNull(2) ? null : reader2.GetInt32(2);
                    login.Password = reader2.IsDBNull(3) ? null : reader2.GetString(3);
                }

                reader2.Close();

                conn.Close();

                return login;
            }
		}

        public async Task<int> Insert(LoginSchema insertData)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder();

                query.Append("INSERT INTO Login (UserID, AdminID, password) ");
                query.Append("VALUES (@UserID, @AdminID, @password;");

                var cmd = new SqlCommand(query.ToString(), conn);

                List<SqlParameter> columns = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", insertData.UserID),
                    new SqlParameter("@AdminID", insertData.AdminID),
                    new SqlParameter("@password", insertData.Password ?? string.Empty),
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

        public async Task UpdatePassword(LoginSchema updateData)
        {
            using (var conn = new SqlConnection(this.ConnectionString))
            {
                conn.Open();

                StringBuilder query = new StringBuilder();

                query.Append("UPDATE Login SET password = @Password WHERE UserId = @UserId;");

                var cmd = new SqlCommand(query.ToString(), conn);

                List<SqlParameter> columns = new List<SqlParameter>
                {
                    new SqlParameter("@UserID", updateData.UserID),
                    new SqlParameter("@Password", updateData.Password),
                };

                foreach (var column in columns)
                {
                    cmd.Parameters.Add(column);
                }

                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}

