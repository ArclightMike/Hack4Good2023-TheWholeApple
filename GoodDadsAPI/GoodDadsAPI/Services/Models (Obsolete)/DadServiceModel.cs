using System;
namespace GoodDadsAPI.Services.Models
{
    public class DadServiceModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string PrimaryPhone { get; set; }

        public string WorkPhone { get; set; }

        public string AlternateContact { get; set; }

        public string Email { get; set; }

        public IEnumerable<DependentServiceModel> Dependents { get; set; } = Enumerable.Empty<DependentServiceModel>();

        public string Employer { get; set; }

        public MaritalStatusServiceModel MaritalStatus { get; set; }
    }
}

