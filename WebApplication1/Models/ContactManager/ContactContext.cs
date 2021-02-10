

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, firstName = "hernan", lastName = "zavala" },
                new User { Id = 2, firstName = "ryan", lastName = "burk" }
                );
            modelBuilder.Entity<Address>().HasData(
             new Address { personID = 1, city = "smithtown", Street = "1029 jericho", State = "ny", postalCode = "11787" },
           new Address { personID = 2, city = "smithtown", Street = "1029 jericho", State = "ny", postalCode = "11787" }
             );
        }
    }

}
