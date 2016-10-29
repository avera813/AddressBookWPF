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
    /// Interaction logic for AddAddress.xaml
    /// </summary>
    public partial class AddAddress : Page
    {
        XmlDocument xmlDoc;
        public AddAddress()
        {
            InitializeComponent();
        }

        public AddAddress(XmlDocument xmlDoc):this()
        {
            this.xmlDoc = xmlDoc;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(name.Text) || String.IsNullOrEmpty(street.Text) || String.IsNullOrEmpty(city.Text) || String.IsNullOrEmpty(state.Text) || String.IsNullOrEmpty(zip.Text) || String.IsNullOrEmpty(country.Text))
            {
                string errorMessage = "";

                if (String.IsNullOrEmpty(name.Text))
                    errorMessage += "Please provide a name." + System.Environment.NewLine;

                if (String.IsNullOrEmpty(street.Text))
                    errorMessage += "Please provide a street." + System.Environment.NewLine;

                if (String.IsNullOrEmpty(city.Text))
                    errorMessage += "Please provide a city." + System.Environment.NewLine;

                if (String.IsNullOrEmpty(state.Text))
                    errorMessage += "Please provide a state." + System.Environment.NewLine;

                if (String.IsNullOrEmpty(zip.Text))
                    errorMessage += "Please provide a zip." + System.Environment.NewLine;

                if (String.IsNullOrEmpty(country.Text))
                    errorMessage += "Please provide a country.";

                MessageBox.Show(errorMessage);
            }
            else
            {
                XmlDocumentFragment data = xmlDoc.CreateDocumentFragment();
                XmlNodeList nodes = xmlDoc.SelectNodes("//Person[@Name='" + name.Text + "']");

                if (nodes.Count > 0)
                {
                    data.InnerXml = @String.Format("<Address Street=\"{0}\" City=\"{1}\" State=\"{2}\" Zip=\"{3}\" Country=\"{4}\" />",
                                                    street.Text,
                                                    city.Text,
                                                    state.Text,
                                                    zip.Text,
                                                    country.Text);

                    xmlDoc.SelectSingleNode("//Person[@Name='" + name.Text + "']").AppendChild(data);
                }
                else
                {
                    data.InnerXml = @String.Format("<Person Name=\"{0}\"><Address Street=\"{1}\" City=\"{2}\" State=\"{3}\" Zip=\"{4}\" Country=\"{5}\" /></Person>",
                                                    name.Text,
                                                    street.Text,
                                                    city.Text,
                                                    state.Text,
                                                    zip.Text,
                                                    country.Text);

                    xmlDoc.DocumentElement.AppendChild(data);
                }
                
                try
                {
                    using (FileStream writeFile = CheckFile.GetWriteFileStream(@"C:\Addresses.xml"))
                    {
                        xmlDoc.Save(writeFile);
                        writeFile.Close();
                        MessageBox.Show("The entry has been added.");
                        this.NavigationService.Navigate(new ViewAddressBook());
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
