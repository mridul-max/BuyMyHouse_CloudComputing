using System;

namespace BuyMyHouse.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ZipCode { get; set; }
        public float Income { get; set; }
        public string PDF { get; set; }
    }
}
