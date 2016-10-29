using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                    String fileName = openDialog.FileName;
                    XmlDocument xmlDoc = new XmlDocument();
                    using (FileStream file = CheckFile.GetReadFileStream(@fileName))
                    {
                        xmlDoc.Load(file);
                        this.NavigationService.Navigate(new ViewAddressBook(xmlDoc, fileName));
                        file.Close();
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("Invalid Xml - Line: " + ex.LineNumber + ", Position: " + ex.LinePosition);
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
                    String fileName = saveDialog.FileName;
                    XmlDocument xmlDoc = new XmlDocument();
                    using (FileStream file = CheckFile.GetWriteFileStream(@fileName))
                    {
                        xmlDoc.LoadXml("<Addresses></Addresses>");
                        xmlDoc.Save(file);
                        this.NavigationService.Navigate(new ViewAddressBook(xmlDoc, fileName));
                        file.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
