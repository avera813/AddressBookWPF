using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AddressBookWPF
{
    class AddressBook
    {
        AddressBookContext addressBook;
        /*private AddressBookDBDataSet addressBookDbDataSet;
        private AddressBookDBDataSetTableAdapters.PeopleTableAdapter addressBookDbDataSetPeopleTableAdapter;
        private AddressBookDBDataSetTableAdapters.AddressesTableAdapter addressBookDbDataSetAddressesTableAdapter;*/

        public AddressBook()
        {
            addressBook = new AddressBookContext();
            /*addressBookDbDataSet = new AddressBookDBDataSet();
            addressBookDbDataSetPeopleTableAdapter = new AddressBookDBDataSetTableAdapters.PeopleTableAdapter();
            addressBookDbDataSetAddressesTableAdapter = new AddressBookDBDataSetTableAdapters.AddressesTableAdapter();*/
        }
        /*
        private AddressBookDBDataSet.PeopleDataTable GetPeople()
        {
            return addressBookDbDataSetPeopleTableAdapter.GetData();
        }

        private AddressBookDBDataSet.AddressesDataTable GetAddresses()
        {
            return addressBookDbDataSetAddressesTableAdapter.GetData();
        }
        */
        public List<Person> GetEntries()
        {
            return addressBook.People.ToList();
            /*
            IEnumerable<Address> entries =
                from person in GetPeople()
                join address in GetAddresses()
                on person.Id equals address.PersonId
                orderby person.Name ascending
                select new Address { Name = person.Name, Street = address.Street, City = address.City, State = address.State, Zip = address.Zip, Country = address.Country };

            return entries;*/
        }
        /*
        private int GetPersonId(string name)
        {
            List<AddressBookDBDataSet.PeopleRow> foundRows = GetPeople().Where(person => person.Name.ToLower().Equals(name.ToLower())).ToList();
            if(foundRows.Any())
            {
                return foundRows.First().Id;
            }
            return -1;
        }

        private int GetAddressId(int personId)
        {
            List<AddressBookDBDataSet.AddressesRow> foundRows = GetAddresses().Where(item => item.PersonId.Equals(personId)).ToList();
            if (foundRows.Any())
            {
                return foundRows.First().Id;
            }
            return -1;
        }

        public void Add(Address address)
        {
            address.Validate();

            int personId = GetPersonId(address.Name);

            if (personId < 0)
            {
                addressBookDbDataSetPeopleTableAdapter.Insert(address.Name);
                personId = GetPersonId(address.Name);
                addressBookDbDataSetAddressesTableAdapter.Insert(address.Street, address.City, address.State, address.Zip, address.Country, personId);
            }
            else
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }

        }

        public void Update(string oldName, Address address)
        {
            address.Validate();

            int personId = GetPersonId(address.Name);

            if ( personId < 0 || oldName.ToLower().Equals(address.Name.ToLower()) )
            {
                personId = GetPersonId(oldName);
                int addressId = GetAddressId(personId);

                AddressBookDBDataSet.AddressesRow addressRow = GetAddresses().Where(item => item.Id.Equals(addressId)).First();
                addressRow.Street = address.Street;
                addressRow.City = address.City;
                addressRow.State = address.State;
                addressRow.Zip = address.Zip;
                addressRow.Country = address.Country;

                addressBookDbDataSetPeopleTableAdapter.Update(address.Name, personId, oldName);
                addressBookDbDataSetAddressesTableAdapter.Update(addressRow);
            }
            else
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }
        }

        public void Delete(Address address)
        {
            int personId = GetPersonId(address.Name);
            int addressId = GetAddressId(personId);
            addressBookDbDataSetAddressesTableAdapter.Delete(addressId, address.Street, address.City, address.State, address.Zip, address.Country, personId);
            addressBookDbDataSetPeopleTableAdapter.Delete(personId, address.Name);
        }
        */
    }
}