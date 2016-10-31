using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        private static void ValidatePerson(XmlDocument xmlDoc, string name, string oldName = "")
        {
            List<XmlElement> nodes = xmlDoc.SelectNodes("//Person").Cast<XmlElement>().Where(item => item.GetAttribute("Name").ToLower().Equals(name.ToLower())).ToList();

            if((!String.IsNullOrEmpty(oldName) && nodes.Count > 0 && !name.ToLower().Equals(oldName.ToLower())) || (String.IsNullOrEmpty(oldName) && nodes.Count > 0))
                throw new ArgumentException("An entry with the same name already exists.");
        }

        public static XmlElement CreateAddressElement(XmlDocument xmlDoc, string street, string city, string state, string zip, string country)
        {
            XmlElement addressElem = xmlDoc.CreateElement("Address");
            addressElem.SetAttribute("Street", street);
            addressElem.SetAttribute("City", city);
            addressElem.SetAttribute("State", state);
            addressElem.SetAttribute("Zip", zip);
            addressElem.SetAttribute("Country", country);
            return addressElem;
        }

        public static XmlElement CreateNewPerson(XmlDocument xmlDoc, string name, string oldName = "")
        {
            XmlElement personElem;
            ValidatePerson(xmlDoc, name, oldName);
            if(!String.IsNullOrEmpty(oldName))
            {
                personElem = xmlDoc.SelectSingleNode("Addresses").SelectNodes("Person").Cast<XmlElement>().Where(item => item.GetAttribute("Name").Equals(oldName)).First();
                if (personElem.HasChildNodes)
                    personElem.RemoveAll();
            }
            else
            {
                personElem = xmlDoc.CreateElement("Person");
            }
            
            personElem.SetAttribute("Name", name);
            return personElem;
        }
    }
}