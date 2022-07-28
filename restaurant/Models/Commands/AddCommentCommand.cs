using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Models.Commands
{
    public class AddCommentCommand
    {
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int RestaurantId { get; set; }
    }
}
