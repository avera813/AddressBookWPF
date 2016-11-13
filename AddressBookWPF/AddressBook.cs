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
        private AddressBookDBDataSet addressBookDbDataSet;
        private AddressBookDBDataSetTableAdapters.PersonTableAdapter addressBookDbDataSetPersonTableAdapter;
        private AddressBookDBDataSetTableAdapters.AddressTableAdapter addressBookDbDataSetAddressTableAdapter;

        public AddressBook()
        {
            addressBookDbDataSet = new AddressBookDBDataSet();
            addressBookDbDataSetPersonTableAdapter = new AddressBookDBDataSetTableAdapters.PersonTableAdapter();
            addressBookDbDataSetAddressTableAdapter = new AddressBookDBDataSetTableAdapters.AddressTableAdapter();
        }

        private AddressBookDBDataSet.PersonDataTable GetPeople()
        {
            return addressBookDbDataSetPersonTableAdapter.GetData();
        }

        public AddressBookDBDataSet.AddressDataTable GetAddresses()
        {
            return addressBookDbDataSetAddressTableAdapter.GetData();
        }
        
        public IEnumerable<Address> GetContactList()
        {
            IEnumerable<Address> entries =
                from person in GetPeople()
                join address in GetAddresses()
                on person.Id equals address.PersonId
                orderby person.Name ascending
                select new Address(person.Name, address.Street, address.City, address.State, address.Zip, address.Country);

            return entries;
        }
        
        private int GetPersonId(string name)
        {
            List<AddressBookDBDataSet.PersonRow> foundRows = GetPeople().Where(person => person.Name.ToLower().Equals(name.ToLower())).ToList();
            if(foundRows.Any())
            {
                return foundRows.First().Id;
            }
            return -1;
        }

        private int GetAddressId(int personId)
        {
            List<AddressBookDBDataSet.AddressRow> foundRows = GetAddresses().Where(item => item.PersonId.Equals(personId)).ToList();
            if (foundRows.Any())
            {
                return foundRows.First().Id;
            }
            return -1;
        }

        public void Add(string name, string street, string city, string state, string zip, string country)
        {
            Address address = new Address(name, street, city, state, zip, country);
            int personId = GetPersonId(address.Name);

            address.Validate();

            if (personId < 0)
            {
                addressBookDbDataSetPersonTableAdapter.Insert(address.Name);
                addressBookDbDataSetAddressTableAdapter.Insert(address.Street, address.City, address.State, address.Zip, address.Country, personId);
            }
            else
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }

        }

        public void Update(string oldName, Address address)
        {
            address.Validate();

            if ( GetPersonId(address.Name) < 0 || oldName.ToLower().Equals(address.Name.ToLower()) )
            {
                int personId = GetPersonId(oldName);
                int addressId = GetAddressId(personId);

                AddressBookDBDataSet.AddressRow addressRow = GetAddresses().Where(item => item.Id.Equals(addressId)).First();
                addressRow.Street = address.Street;
                addressRow.City = address.City;
                addressRow.State = address.State;
                addressRow.Zip = address.Zip;
                addressRow.Country = address.Country;

                addressBookDbDataSetPersonTableAdapter.Update(address.Name, personId, oldName);
                addressBookDbDataSetAddressTableAdapter.Update(addressRow);
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
            addressBookDbDataSetAddressTableAdapter.Delete(addressId, address.Street, address.City, address.State, address.Zip, address.Country, personId);
            addressBookDbDataSetPersonTableAdapter.Delete(personId, address.Name);
        }
    }
}