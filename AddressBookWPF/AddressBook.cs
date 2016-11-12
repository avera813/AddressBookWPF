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
        private AddressBookDBDataSet addressBookDbDataSet = new AddressBookDBDataSet();
        private AddressBookDBDataSetTableAdapters.PersonTableAdapter addressBookDbDataSetPersonTableAdapter = new AddressBookDBDataSetTableAdapters.PersonTableAdapter();
        private AddressBookDBDataSetTableAdapters.AddressTableAdapter addressBookDbDataSetAddressTableAdapter = new AddressBookDBDataSetTableAdapters.AddressTableAdapter();
        private IEnumerable<KeyValuePair<string, Address>> entries;

        public AddressBook()
        {
            var people =
                from person in addressBookDbDataSetPersonTableAdapter.GetData()
                select person;

            var addresses =
                from address in addressBookDbDataSetAddressTableAdapter.GetData()
                select address;

            entries =
                from person in people
                join address in addresses
                on person.Id equals address.PersonId
                orderby person.Name ascending
                select new KeyValuePair<string, Address>(person.Name, new Address(address.Street, address.City, address.State, address.Zip, address.Country));
        }
        
        public List<string> GetContactList()
        {
            return (from entry in entries select entry.Key).ToList();
        }

        public void Add()
        {

        }

        public void Update()
        {

        }

        public void Remove()
        {

        }

        public void Find()
        {

        }

        public void Sort()
        {
            entries = from entry in entries
                      orderby entry.Key
                      select entry;
        }
    }
}
