using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for EditAddress.xaml
    /// </summary>
    public partial class EditAddress : Page
    {
        string oldName;
        string fileName;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data, string fileName) : this()
        {
            this.DataContext = data;
            this.fileName = fileName;
            oldName = (data as XmlElement).GetAttribute("Name");
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddressForm.Validate(name.Text, street.Text, city.Text, state.Text, zip.Text, country.Text);
                XmlDocument xmlDoc = ReaderWriter.GetXmlDocument(fileName);
                List<XmlElement> nodes = xmlDoc.SelectNodes("//Person").Cast<XmlElement>().Where(item => item.GetAttribute("Name").ToLower().Equals(name.Text.ToLower())).ToList();

                // If name change occurs, make sure it does not already exist in the address book
                if (nodes.Count > 0 && !name.Text.ToLower().Equals(oldName.ToLower()))
                    throw new ArgumentException("An entry with the same name already exists.");

                // Set current node based on initial name value
                XmlElement currElem = xmlDoc.SelectSingleNode("Addresses").SelectNodes("Person").Cast<XmlElement>().Where(item=>item.GetAttribute("Name").Equals(oldName)).First();
                if(currElem.HasChildNodes)
                    currElem.RemoveAll();

                currElem.SetAttribute("Name", name.Text);    

                // Create new address element and add to currElem
                XmlElement addressElem = xmlDoc.CreateElement("Address");
                addressElem.SetAttribute("Street", street.Text);
                addressElem.SetAttribute("City", city.Text);
                addressElem.SetAttribute("State", state.Text);
                addressElem.SetAttribute("Zip", zip.Text);
                addressElem.SetAttribute("Country", country.Text);
                currElem.PrependChild(addressElem);
                        
                ReaderWriter.WriteXmlToDocument(fileName, xmlDoc);
                MessageBox.Show("The entry has been updated.");
                this.NavigationService.Navigate(new ViewAddressBook(fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook(fileName));
        }
    }
}
