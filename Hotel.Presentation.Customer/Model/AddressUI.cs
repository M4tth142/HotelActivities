using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Presentation.Customer.Model
{
    public class AddressUI
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }

        public AddressUI(string street, string zipCode, string city, string houseNumber)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
            HouseNumber = houseNumber;
        }
    }
}
