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
    /// Interaction logic for ViewAddress.xaml
    /// </summary>
    public partial class ViewAddress : Page
    {
        XmlDocument xmlDoc;
        string fileName;

        public ViewAddress()
        {
            InitializeComponent();
        }

        public ViewAddress(object data, XmlDocument xmlDoc, string fileName):this()
        {
            this.DataContext = data;
            this.xmlDoc = xmlDoc;
            this.fileName = fileName;
        }

        private void returnToAddressBook_Click(object sender, RoutedEventArgs e)
        {
            ViewAddressBook viewAddressBookPage = new ViewAddressBook(xmlDoc,fileName);
            this.NavigationService.Navigate(viewAddressBookPage);
        }
    }
}
