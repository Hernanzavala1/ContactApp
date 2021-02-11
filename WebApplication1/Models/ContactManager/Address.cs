using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ContactManager
{
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
        public int User{ get; set; }
    }
}
