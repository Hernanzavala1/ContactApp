

using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.ContactManager
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }
  
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
  
}
