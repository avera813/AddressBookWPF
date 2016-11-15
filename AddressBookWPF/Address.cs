namespace AddressBookWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Street { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }

        [Required]
        [StringLength(255)]
        public string State { get; set; }

        [Required]
        [StringLength(255)]
        public string Zip { get; set; }

        [Required]
        [StringLength(255)]
        public string Country { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }
    }
}
