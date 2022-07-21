using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restaurant.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Restaurant Restaurant { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        
        public string Description { get; set; }

    }
}
