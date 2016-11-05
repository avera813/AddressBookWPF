using System;

namespace AddressBookWPF
{
    class AddressForm
    {
        private AddressForm() { }
        public static void Validate(string name, string street, string city, string state, string zip, string country)
        {
            string errorMessage = "";

            if (String.IsNullOrEmpty(name))
                errorMessage += "Please provide a name." + System.Environment.NewLine;

            if (String.IsNullOrEmpty(street))
                errorMessage += "Please provide a street." + System.Environment.NewLine;

            if (String.IsNullOrEmpty(city))
                errorMessage += "Please provide a city." + System.Environment.NewLine;

            if (String.IsNullOrEmpty(state))
                errorMessage += "Please provide a state." + System.Environment.NewLine;

            if (String.IsNullOrEmpty(zip))
                errorMessage += "Please provide a zip." + System.Environment.NewLine;

            if (String.IsNullOrEmpty(country))
                errorMessage += "Please provide a country.";

            if(!String.IsNullOrEmpty(errorMessage))
                throw new ArgumentException(errorMessage);
        }
    }
}