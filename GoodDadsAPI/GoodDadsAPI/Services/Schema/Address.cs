namespace GoodDadsAPI.Services.Schema
{
    public class Address
    {
        public int UserID { get; set; }

        public int AddressID { get; set; }

        public string LineOne { get; set; }

        public string LineTwo { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}
