using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF
{
    class Address
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

        public Address(){}

        public void Validate()
        {
            string errorMessage = "";

            if (String.IsNullOrWhiteSpace(Name))
                errorMessage += "Please provide a name." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(Street))
                errorMessage += "Please provide a street." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(City))
                errorMessage += "Please provide a city." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(State))
                errorMessage += "Please provide a state." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(Zip))
                errorMessage += "Please provide a zip." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(Country))
                errorMessage += "Please provide a country.";

            if (!String.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException(errorMessage);
        }
    }
}
