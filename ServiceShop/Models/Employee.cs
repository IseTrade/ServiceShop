using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ServiceShop.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }   //Primary key
        [Display(Name = "Employee Name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public double? Rating { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}