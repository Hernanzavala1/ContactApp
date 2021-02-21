using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ContactManager
{
    //This is the model for the Address Table in our database
    public class Address
    {
        [Key]
        public int personID { get; set; }
        [Required]
        public String Street { get; set; }
        [Required]
        public String city { get; set; }
        [Required]
        public String State { get; set; }
        [Required]
        public String postalCode { get; set; }
        //The User attribute is a foreign key to the User table. This is so we can retrieve a certain user tied to this address.
        public int User{ get; set; }
    }
}
