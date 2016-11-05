﻿using System;
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
            ValidatePerson(name);

            int nextId = (int)addressBookDbDataSetPersonTableAdapter.GetData().Rows[addressBookDbDataSetPersonTableAdapter.GetData().Count - 1][0] + 1;
            addressBookDbDataSetPersonTableAdapter.Insert(nextId, name, city, country, state, street, zip);
        }

        public static void UpdateEntry(AddressBookDBDataSet.PersonRow personRow, string oldName)
        {
            AddressForm.Validate(personRow.Name, personRow.Street, personRow.City, personRow.State, personRow.Zip, personRow.Country);
            ValidatePerson(personRow.Name, oldName);
            addressBookDbDataSetPersonTableAdapter.Update(personRow);
        }

        public static void DeleteEntry(AddressBookDBDataSet.PersonRow personRow)
        {
            addressBookDbDataSetPersonTableAdapter.Delete(personRow.Id, personRow.Name, personRow.City, personRow.Country, personRow.State, personRow.Street, personRow.Zip);
        }

        private static void ValidatePerson(string name, string oldName = "")
        {
            int foundEntries = addressBookDbDataSetPersonTableAdapter.GetData().Rows.Cast<DataRow>().Where(item => item[1].ToString().ToLower().Equals(name.ToLower())).ToList().Count;
           
            if ((foundEntries > 0 && String.IsNullOrWhiteSpace(oldName)) || (foundEntries > 0 && !String.IsNullOrWhiteSpace(oldName) && !oldName.ToLower().Equals(name.ToLower())))
            {
                throw new ArgumentException("An entry with the same name already exists.");
            }
        }
    }
}
