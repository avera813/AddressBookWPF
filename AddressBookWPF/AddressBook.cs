using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Contexts;

namespace AddressBookWPF
{
    class AddressBook
    {
        AddressBookContext addressBook;

        public AddressBook()
        {
            addressBook = new AddressBookContext();
        }

        public List<Person> GetEntries()
        {
            return addressBook.People.ToList();
        }

        public void Add(string name, string street, string city, string state, string zip, string country)
        {
            // Create new person and give the person a name
            Person person = new Person { Name = name };

            // Create new address and add to person's addresses
            Address address = new Address { Street = street, City = city, State = state, Zip = zip, Country = country };
            person.Addresses.Add(address);
            
            // Validate the person and properties
            ValidatePerson(person);

            // If entry already exists, just add a new address to it
            if (CheckIfPersonExists(person))
            {
                // Assign person to the one found in the address book
                person = FindContact(name);

                // Check if address already exists for the person
                if (person.CheckIfAddressExists(address))
                    throw new ArgumentException("The person and address are already in the address book.");
                else
                    person.Addresses.Add(address);
            }
            else
            {
                addressBook.People.Add(person);
            }

            addressBook.SaveChanges();
        }
        
        public void Update(string oldName, Person person)
        {
            ValidatePerson(person);

            if (!CheckIfPersonExists(person) || oldName.ToLower().Equals(person.Name.ToLower()))
            {
                Person currentPerson = addressBook.People.Where(item => item.Id.Equals(person.Id)).First();
                addressBook.People.Attach(currentPerson);
                currentPerson.Name = person.Name;

                for (var i = 0; i < currentPerson.Addresses.Count; ++i)
                {
                    var currentAddress = currentPerson.Addresses.ElementAt(i);
                    var newAddress = person.Addresses.ElementAt(i);
                    currentAddress.Street = newAddress.Street;
                    currentAddress.City = newAddress.City;
                    currentAddress.State = newAddress.State;
                    currentAddress.Zip = newAddress.Zip;
                    currentAddress.Country = newAddress.Country;
                }

                addressBook.SaveChanges();
            }
            else
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }
        }
        
        public void Delete(Person person)
        {
            addressBook.Addresses.RemoveRange(person.Addresses);
            addressBook.People.Remove(person);
            addressBook.SaveChanges();
        }

        private bool CheckIfPersonExists(Person person)
        {
            List<Person> foundNames = addressBook.People.Where(item => item.Name.ToLower().Equals(person.Name.ToLower())).ToList();
            if (foundNames.Any())
                return true;
            
            return false;
        }

        private Person FindContact(string name)
        {
            Person foundPerson = addressBook.People.Where(item => item.Name.ToLower().Equals(name.ToLower())).First();
            return foundPerson;
        }

        public void ValidatePerson(Person person)
        {
            string errorMessage = "";

            if(String.IsNullOrWhiteSpace(person.Name))
                errorMessage += "Please provide a name." + System.Environment.NewLine;

            if (person.Addresses.Any())
            {
                var addresses = person.Addresses;

                for (var i = 0; i < addresses.Count; ++i)
                {
                    errorMessage += System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Street))
                        errorMessage += "Please provide a street for row: " + i + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).City))
                        errorMessage += "Please provide a city for row: " + i + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).State))
                        errorMessage += "Please provide a state for row: " + i + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Zip))
                        errorMessage += "Please provide a zip for row: " + i + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Country))
                        errorMessage += "Please provide a country for row: " + i;
                }
            }

            if (!String.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException(errorMessage);
        }
    }
}