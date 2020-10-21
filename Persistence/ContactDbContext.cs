using AddressBookWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AddressBookWebApi.Persistence
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options ) : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; } 
    }
}
