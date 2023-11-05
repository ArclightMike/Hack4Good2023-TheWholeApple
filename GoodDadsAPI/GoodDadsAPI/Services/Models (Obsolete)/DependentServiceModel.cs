namespace GoodDadsAPI.Services.Models
{
    public class DependentServiceModel
    {
        //public int DependentId { get; set; }

        //public int UserId { get; set; }

        public DadServiceModel Dad { get; set; }

        public DateTime BirthDate { get; set; }

        //public int ContactStatusId { get; set; }

        public int SupportPerMonth { get; set; }
    }
}
