using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactApp.viewModel
{
    public class ContactVM
    {
        //view model for the contact to be created
        public int id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public String firstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public String lastName { get; set; }
        [Display(Name = "Street")]
        [Required]
        public String streetName { get; set; }
        [Display(Name = "City")]
        [Required]
        public String city { get; set; }
        [Display(Name = "State")]
        [Required]
        public String state { get; set; }
        [Display(Name = "Postal Code")]
        [Required]
        public String zipCode { get; set; }
    }
}
