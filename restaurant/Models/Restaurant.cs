using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant.Models
{
    public class Restaurant
    {
        
        public Restaurant()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get;  set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string WorkHours { get; set; }

        [Required]
        public string Menu { get; set; }

        [Required]
        public int Capacity { get;  set; }

        public int EmptySeats { get; set; } 

    }
}
