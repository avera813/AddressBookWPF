namespace AddressBookWPF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AddressBookContext : DbContext
    {
        public AddressBookContext()
            : base("name=AddressBookContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
