using System;

namespace BuyMyHouse.Model
{
    public class House
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string ImageURL { get; set; }
    }
}
