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
            List<Person> sortedEntries = addressBook.People.OrderBy(item => item.Name).ToList();
            return sortedEntries;
        }

        public void Add(string name, string street, string city, string state, string zip, string country)
        {
            // Attempt to find person in address book and assign to person variable
            Person person = FindContact(name);

            // Create new address based on passed information
            Address address = new Address { Street = street, City = city, State = state, Zip = zip, Country = country };

            // Check if person is null (not found)
            if(person == null)
            {
                // Assign new person to existing person object
                person = new Person { Name = name };

                // Add address to the new person
                person.Addresses.Add(address);

                // Add person to address book
                addressBook.People.Add(person);
            }
            else
            {
                // Check if address already exists for the person
                if (person.CheckIfAddressExists(address))
                    throw new ArgumentException("The person and address are already in the address book.");
                else
                    person.Addresses.Add(address);
            }

            // Validate the person and properties
            ValidatePerson(person);

            // Save the changes
            addressBook.SaveChanges();
        }
        
        public void Update(string oldName, Person person)
        {
            ValidatePerson(person);

            if (FindContact(person.Name) == null || oldName.ToLower().Equals(person.Name.ToLower()))
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

        private Person FindContact(string name)
        {
            var foundEntries = addressBook.People.Where(item => item.Name.ToLower().Equals(name.ToLower()));
            if(foundEntries.Any())
                return addressBook.People.Where(item => item.Name.ToLower().Equals(name.ToLower())).First();
            return null;
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
                    var row = i + 1;

                    errorMessage += System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Street))
                        errorMessage += "Please provide a street for row: " + row + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).City))
                        errorMessage += "Please provide a city for row: " + row + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).State))
                        errorMessage += "Please provide a state for row: " + row + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Zip))
                        errorMessage += "Please provide a zip for row: " + row + System.Environment.NewLine;

                    if (String.IsNullOrWhiteSpace(addresses.ElementAt(i).Country))
                        errorMessage += "Please provide a country for row: " + row;
                }
            }

            if (!String.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException(errorMessage);
        }
    }
}