using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models.ContactManager
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

    }
}
