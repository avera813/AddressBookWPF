using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF
{
    [Serializable]
    public class AddressBook
    {
        private Dictionary<string, Address> addresses;

        public AddressBook()
        {
            addresses = new Dictionary<string, Address>();
        }

        public Dictionary<string, Address> GetAll()
        {
            return addresses;
        }

        public void Add(string name, Address address)
        {
            if (!addresses.ContainsKey(name))
                addresses.Add(name, address);
        }

        public void Update(string name, string keyToUpdate, string newValue)
        {
            if (addresses.ContainsKey(name))
            {
                if (!keyToUpdate.Equals("name"))
                {
                    addresses[name].setSpec(keyToUpdate, newValue);
                }
                else
                {
                    Address tempAddress = addresses[name];
                    addresses.Remove(name);
                    addresses.Add(newValue, tempAddress);
                }
            }
        }

        public void Remove(string name)
        {
            if (addresses.ContainsKey(name))
                addresses.Remove(name);
        }

        public Dictionary<string, Address> Find(string key, string query)
        {
            key = key.ToLower();
            query = query.ToLower();

            Dictionary<string, Address> tempAddressBook = addresses.Where(pair => key.Equals("name") && pair.Key.ToLower().Contains(query) || !key.Equals("name") && pair.Value.getSpec(key).ToLower().Contains(query)).ToDictionary(pair => pair.Key, pair => pair.Value);

            return tempAddressBook;
        }

        public void Sort(string key)
        {
            key = key.ToLower();
            if (key.Equals("name"))
                addresses = addresses.OrderBy(prop => prop.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
            else
                addresses = addresses.OrderBy(prop => prop.Value.getSpec(key)).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
