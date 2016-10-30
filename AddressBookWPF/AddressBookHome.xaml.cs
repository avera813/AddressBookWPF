using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for AddressBookHome.xaml
    /// </summary>
    public partial class AddressBookHome : Page
    {
        public AddressBookHome()
        {
            InitializeComponent();
        }

        private void loadAddressBook_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "XML Files (*.xml)|*.xml";
            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openDialog.FileName;
                    ReaderWriter.GetXmlDocument(fileName);
                    this.NavigationService.Navigate(new ViewAddressBook(fileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void createAddressBook_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML Files (*.xml)|*.xml";
            if (saveDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = saveDialog.FileName;
                    ReaderWriter.CreateXmlDocument(fileName);
                    ReaderWriter.GetXmlDocument(fileName);
                    this.NavigationService.Navigate(new ViewAddressBook(fileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
