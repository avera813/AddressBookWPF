using System;
using System.Data;
using System.Linq;

namespace AddressBookWPF
{
    class AddressBookData
    {
        private static AddressBookDBDataSet addressBookDbDataSet = new AddressBookDBDataSet();
        private static AddressBookDBDataSetTableAdapters.PersonTableAdapter addressBookDbDataSetPersonTableAdapter = new AddressBookDBDataSetTableAdapters.PersonTableAdapter();

        private AddressBookData() { }

        public static AddressBookDBDataSet.PersonDataTable GetEntries()
        {
            return addressBookDbDataSetPersonTableAdapter.GetData();
        }

        public static void AddEntry(string name, string street, string city, string state, string zip, string country)
        {
            AddressForm.Validate(name, street, city, state, zip, country);
            AddressForm.ValidatePerson(name);
           
            try
            {
                addressBookDbDataSetPersonTableAdapter.Insert(name, street, city, state, zip, country);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to add the address book entry for {0}", name));
            }
        }

        public static void UpdateEntry(AddressBookDBDataSet.PersonRow personRow, string oldName)
        {
            AddressForm.Validate(personRow.Name, personRow.Street, personRow.City, personRow.State, personRow.Zip, personRow.Country);
            AddressForm.ValidatePerson(personRow.Name, oldName);
            try
            {
                addressBookDbDataSetPersonTableAdapter.Update(personRow);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to update the address book entry for {0}", personRow.Name));
            }
        }

        public static void DeleteEntry(AddressBookDBDataSet.PersonRow personRow)
        {
            try
            {
                addressBookDbDataSetPersonTableAdapter.Delete(personRow.Id, personRow.Name, personRow.Street, personRow.City, personRow.State, personRow.Zip, personRow.Country);
            }
            catch (Exception)
            {
                throw new Exception(String.Format("Unable to delete the address book entry for {0}", personRow.Name));
            }
        }
    }
}
