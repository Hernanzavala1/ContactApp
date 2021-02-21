

using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models.ContactManager
{
    //This will be the context class where from which the database will be based off.
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {

        }
        //The database will have a set of Users and also a set of Addresses pertaining to users in the database.
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        //The following method is responsible for seeding the database with 2 records
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, firstName = "hernan", lastName = "zavala" },
                new User { Id = 2, firstName = "ryan", lastName = "burk" }
                );
            modelBuilder.Entity<Address>().HasData(
             new Address { personID = 1, city = "smithtown", Street = "1029 jericho", State = "ny", postalCode = "11787",User=1 },
           new Address { personID = 2, city = "smithtown", Street = "1029 jericho", State = "ny", postalCode = "11787",User=2 }
             );
        }
    }

}
