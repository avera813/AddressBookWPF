using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for ViewAddress.xaml
    /// </summary>
    public partial class ViewAddress : Page
    {
        string fileName;

        public ViewAddress()
        {
            InitializeComponent();
        }

        public ViewAddress(object data, string fileName) : this()
        {
            this.DataContext = data;
            this.fileName = fileName;
        }

        private void returnToAddressBook_Click(object sender, RoutedEventArgs e)
        {
            ViewAddressBook viewAddressBookPage = new ViewAddressBook(fileName);
            this.NavigationService.Navigate(viewAddressBookPage);
        }
    }
}
