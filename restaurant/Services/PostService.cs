using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using restaurant.Data;
using restaurant.Models;

namespace restaurant.Services
{
    public class PostService
    {
        //imitira baza:
        private List<Post> dbContext_dbSetPost = new List<Post>();

        public PostService()
        {
            for (int i = 0; i < 4; i++)
            {
                Restaurant restaurant = new Restaurant();
                restaurant.Name = "re"+i;
                restaurant.Phone = i+"";

                Post post = new Post();
                post.Restaurant = restaurant;
                dbContext_dbSetPost.Add(post);
            }
        }

        public List<Post> FilteredPosts(string filterType, string searchedWord) 
        {
            
            List<Post> filteredPosts = new List<Post>();

            dbContext_dbSetPost.ForEach(delegate(Post p) {
             object valueForComparison=   p.Restaurant.GetType()
                .GetProperty(filterType).GetValue(p.Restaurant,null);

                if (valueForComparison.Equals(searchedWord))
                {
                    filteredPosts.Add(p);
                }
                
            });

            return filteredPosts;
        }
    }
}
