namespace GoodDadsAPI.Services.Schema
{
    public class LoginSchema
    {
        public int LoginID { get; set; }

        public int? UserID { get; set; }

        public int? AdminID { get; set; }

        public string? Password { get; set; }
    }
}
