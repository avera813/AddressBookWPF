using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF
{
    class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public string Country { get; private set; }

        public Address(string street, string city, string state, string zip, string country)
        {
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Country = country;
        }
        //private static AddressBookDBDataSet addressBookDbDataSet = new AddressBookDBDataSet();
        //private static AddressBookDBDataSetTableAdapters.AddressTableAdapter addressBookDbDataSetAddressTableAdapter = new AddressBookDBDataSetTableAdapters.AddressTableAdapter();
    }
}
