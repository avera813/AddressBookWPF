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
    /// Interaction logic for EditAddress.xaml
    /// </summary>
    public partial class EditAddress : Page
    {
        XmlDocument xmlDoc;

        public EditAddress()
        {
            InitializeComponent();
        }

        public EditAddress(object data, XmlDocument xmlDoc) : this()
        {
            this.DataContext = data;
            this.xmlDoc = xmlDoc;
            XmlAttributeCollection address = (data as XmlElement).SelectSingleNode("Address").Attributes;
            street.Text = address.GetNamedItem("Street").Value;
            city.Text = address.GetNamedItem("City").Value;
            state.Text = address.GetNamedItem("State").Value;
            zip.Text = address.GetNamedItem("Zip").Value;
            country.Text = address.GetNamedItem("Country").Value;
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
                XmlNode currNode = xmlDoc.SelectSingleNode("//Person[@Name='" + name.Text + "']");

                xmlDoc.FirstChild.RemoveChild(currNode);

                currNode.InnerXml = @String.Format("<Address Street=\"{0}\" City=\"{1}\" State=\"{2}\" Zip=\"{3}\" Country=\"{4}\" />",
                                                street.Text,
                                                city.Text,
                                                state.Text,
                                                zip.Text,
                                                country.Text);

                xmlDoc.FirstChild.AppendChild(currNode);
                
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ViewAddressBook());
        }
    }
}
