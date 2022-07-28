using restaurant.Data;
using restaurant.Models;
using restaurant.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Services
{
    public class CommentService
    {

        private DBRContext dbrContext;
        private RestaurantService restaurantService;

        public CommentService(DBRContext dbrContext)
        {
            this.dbrContext = dbrContext;
            this.restaurantService = new RestaurantService(dbrContext);
        }

        public async Task AddComment(AddCommentCommand cmd)
        {
            var newComment = new Comment
            {
                CommentMessage = cmd.Comment,
                RestaurantId = cmd.RestaurantId,
                UserId = cmd.UserId
            };
            dbrContext.comment.Add(newComment);
            await dbrContext.SaveChangesAsync();
        }
    }
}
