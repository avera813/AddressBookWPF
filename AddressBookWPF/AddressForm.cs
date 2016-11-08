using System;
using System.Data;
using System.Linq;

namespace AddressBookWPF
{
    class AddressForm
    {
        private AddressForm() { }
        public static void Validate(string name, string street, string city, string state, string zip, string country)
        {
            string errorMessage = "";

            if (String.IsNullOrWhiteSpace(name))
                errorMessage += "Please provide a name." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(street))
                errorMessage += "Please provide a street." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(city))
                errorMessage += "Please provide a city." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(state))
                errorMessage += "Please provide a state." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(zip))
                errorMessage += "Please provide a zip." + System.Environment.NewLine;

            if (String.IsNullOrWhiteSpace(country))
                errorMessage += "Please provide a country.";

            if(!String.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException(errorMessage);
        }

        public static void ValidatePerson(string name, string oldName = "")
        {
            int foundEntries = AddressBookData.GetEntries().Rows.Cast<DataRow>().Where(item => item[1].ToString().ToLower().Equals(name.ToLower())).ToList().Count;

            if ((foundEntries > 0 && String.IsNullOrWhiteSpace(oldName)) || (foundEntries > 0 && !String.IsNullOrWhiteSpace(oldName) && !oldName.ToLower().Equals(name.ToLower())))
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }
        }
    }
}