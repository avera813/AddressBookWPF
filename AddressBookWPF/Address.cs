using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookWPF
{
    [Serializable]
    public class Address
    {
        private Dictionary<string, string> address;

        // Constructor for an Address
        public Address(string street, string city, string state, string zip, string country)
        {
            address = new Dictionary<string, string>();
            address.Add("street", street);
            address.Add("city", city);
            address.Add("state", state);
            address.Add("zip", zip);
            address.Add("country", country);
        }

        public void setSpec(string key, string value)
        {
            key = key.ToLower();
            if (address.ContainsKey(key))
                address[key] = value;
        }

        public string getSpec(string key)
        {
            key = key.ToLower();
            if (address.ContainsKey(key))
                return address[key];
            return "";
        }

        public Dictionary<string, string> GetAddress()
        {
            return address;
        }

        override public string ToString()
        {
            return String.Join(", ", address.Values);
        }
    }
}
