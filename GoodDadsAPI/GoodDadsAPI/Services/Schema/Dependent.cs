namespace GoodDadsAPI.Services.Schema
{
    public class Dependent
    {
        public int DependentId { get; set; }

        public int UserId { get; set; }

        public DateTime BirthDate { get; set; }

        public int ContactStatusId { get; set; }

        public decimal SupportPerMonth { get; set; }
    }
}
