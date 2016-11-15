using System.Windows;
using System.Windows.Controls;

namespace AddressBookWPF
{
    /// <summary>
    /// Interaction logic for ViewAddress.xaml
    /// </summary>
    public partial class ViewAddress : Page
    {

        public ViewAddress()
        {
            InitializeComponent();
        }

        public ViewAddress(object data) : this()
        {
            this.DataContext = data;
            Person person = data as Person;
            foreach (Address address in person.Addresses)
            {
                MessageBox.Show(address.Street);
            }
            
        }

        private void returnToAddressBook_Click(object sender, RoutedEventArgs e)
        {
            ViewAddressBook viewAddressBookPage = new ViewAddressBook();
            this.NavigationService.Navigate(viewAddressBookPage);
        }
    }
}
