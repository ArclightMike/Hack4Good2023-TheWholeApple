using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanionApp.Models
{
    internal class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street1 { get; set; } = null!;
        public string Street2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public int ZipCode { get; set; }
        public string Phone { get; set; } = null!;
    }
}
