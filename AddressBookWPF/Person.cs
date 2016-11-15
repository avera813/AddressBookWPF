namespace AddressBookWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Person()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }

        public bool CheckIfAddressExists(Address newAddress)
        {
            foreach (Address address in Addresses)
            {
                bool streetEquals = address.Street.ToLower().Equals(newAddress.Street.ToLower());
                bool cityEquals = address.City.ToLower().Equals(newAddress.City.ToLower());
                bool stateEquals = address.State.ToLower().Equals(newAddress.State.ToLower());
                bool zipEquals = address.Zip.ToLower().Equals(newAddress.Zip.ToLower());
                bool countryEquals = address.Country.ToLower().Equals(newAddress.Country.ToLower());

                if (streetEquals && cityEquals && stateEquals && zipEquals && countryEquals)
                    return true;
            }
            
            return false;
        }
    }
}
