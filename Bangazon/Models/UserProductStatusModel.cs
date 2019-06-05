using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models
{
    public class UserProductStatusModel
    {
        public Product Product { get; set; }

        [Display(Name = "Number Sold")]
        public int NumberSold { get; set; }

        [Display(Name = "Average Rating")]
        public double AverageRating { get; set; }
    }
}
