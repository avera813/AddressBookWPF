﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;
using System.ComponentModel;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for AddressBookContents.xaml
    /// </summary>
    public partial class ViewAddressBook : Page
    {
        XmlDocument xmlDoc;
        public ViewAddressBook()
        {
            InitializeComponent();
            try
            {
                xmlDoc = new XmlDocument();
                using (FileStream readFile = CheckFile.GetReadFileStream(@"C:\Addresses.xml", false))
                {
                    xmlDoc.Load(readFile);
                    List<XmlElement> elems = xmlDoc.SelectNodes("Addresses/Person").Cast<XmlElement>().ToList();
                    this.entryListBox.ItemsSource = elems.OrderBy(item => item.GetAttribute("Name").ToString());
                    readFile.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Enables/disables View/Edit address buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox entryList = sender as ListBox;
            if (entryList != null && entryList.SelectedIndex > -1)
            {
                viewAddress.IsEnabled = true;
                editAddress.IsEnabled = true;
            }
            else
            {
                viewAddress.IsEnabled = false;
                editAddress.IsEnabled = false;
            }
        }

        private void editAddress_Click(object sender, RoutedEventArgs e)
        {
            EditAddress editAddressPage = new EditAddress(this.entryListBox.SelectedItem, xmlDoc);
            this.NavigationService.Navigate(editAddressPage);
        }

        private void viewAddress_Click(object sender, RoutedEventArgs e)
        {
            ViewAddress viewAddressPage = new ViewAddress(this.entryListBox.SelectedItem);
            this.NavigationService.Navigate(viewAddressPage);
        }

        private void addAddress_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddAddress(xmlDoc));
        }
    }
}
