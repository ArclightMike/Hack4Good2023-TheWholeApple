using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntakeApp
{
    internal class SaveIntakeRequest
    { 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address1 { get; set; }

        public string? Address2 { get; set; } = String.Empty;

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string PrimaryPhone { get; set; }

        public string WorkPhone { get; set; }

        public string AlternateContact { get; set; }

        public string Email { get; set; }

        public List<Dependent> Dependents { get; set; } = new List<Dependent>();
    }

    public class Dependent
    {
        public DateOnly BirthDate { get; set; }

        public decimal supportPerMonth { get; set; }
    }
}
