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
        public string RatingData { get; set; } //Mechanism for storing Rating values and count via concatenation
        public string RateTemp { get; set; } //This is to hold the temporary rating value before concat to RatingData
        public double? Rating { get; set; } //This is the calculated avg rating based on RatingData

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}