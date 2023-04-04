using Entities.Model;
using Microsoft.EntityFrameworkCore;
namespace Entities
{
    public class AddressBookContext : DbContext
    {
        public AddressBookContext(DbContextOptions<AddressBookContext> options)
        : base(options)
        {
        }
        
        public DbSet<AddressBook> AddressBook { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<EmailAddress> Email { get; set; }
        public DbSet<PhoneNumber> Phone { get; set; }
        public DbSet<UserLogin> User { get; set; }
        public DbSet<Asset> Asset { get; set; }
    }
}