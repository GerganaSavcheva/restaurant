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
                Restaurant restaurant = new Restaurant("re"+i,i);
                restaurant.Address = i + "";
                restaurant.Type = "type";

                Post post = new Post();
                post.Restaurant = restaurant;
                dbContext_dbSetPost.Add(post);
            }
        }

        public Post GetById(int id)
        {
            return dbContext_dbSetPost.FirstOrDefault(p => p.Id == id);
        }

        public  List<Post> GetAll()
        {
            return dbContext_dbSetPost;
        }

        public List<Post> FilteredPosts(string filterType, string searchedWord) 
        {
            
            List<Post> filteredPosts = new List<Post>();

            if (filterType== "Description")
            {
                dbContext_dbSetPost.ForEach(delegate (Post p) {

                    if (p.Description.ToLower().Contains(searchedWord.ToLower().ToString()))
                    {
                        filteredPosts.Add(p);
                    }

                });
            }
            else
            {
                dbContext_dbSetPost.ForEach(delegate (Post p) {
                    object valueForComparison = p.Restaurant.GetType()
                       .GetProperty(filterType).GetValue(p.Restaurant, null);

                    if (valueForComparison.ToString().ToLower().Contains(searchedWord.ToLower().ToString()))
                    {
                        filteredPosts.Add(p);
                    }

                });
            }

            return filteredPosts;
        }
    }
}
