using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.Data;
using restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace restaurant.Services
{
    public class PostService
    {
        private DBRContext dbrContext;
        private RestaurantService restaurantService;

        public PostService(DBRContext dbrContext)
        {
            this.dbrContext = dbrContext;
            this.restaurantService = new RestaurantService(dbrContext);
            dbrContext.posts.Include(p=> p.Restaurant);
            
        }


        public void Create(Post post)
        {
            post.Restaurant.EmptySeats = post.Restaurant.Capacity;

            dbrContext.posts.Add(post);
            dbrContext.SaveChanges();
        }

        public void Delete(int id)
        {
            restaurantService.Delete(GetById(id).RestaurantId);
        }
        public void Edit(Post post)
        {
            Post oldPost = GetById(post.Id);
            oldPost.Description = post.Description;
       //     restaurantService.Edit(post.Restaurant);
            oldPost.Restaurant.Name = post.Restaurant.Name;
            oldPost.Restaurant.Phone = post.Restaurant.Phone;
            oldPost.Restaurant.Address = post.Restaurant.Address;
            oldPost.Restaurant.Type = post.Restaurant.Type;
            oldPost.Restaurant.WorkHours = post.Restaurant.WorkHours;
            oldPost.Restaurant.Menu = post.Restaurant.Menu;
            oldPost.Restaurant.Capacity = post.Restaurant.Capacity;
            ////oldPost.Restaurant.EmptySeats = post.Restaurant.Capacity;
            dbrContext.SaveChanges();
        }
        public Post GetById(int id)
        {
            return dbrContext.posts.Include(p => p.Restaurant).FirstOrDefault(p => p.Id == id);
        }

        public  List<Post> GetAll()
        {
            return dbrContext.posts.Include(p=>p.Restaurant).ToList<Post>();
        }

        public List<Post> FilteredPosts(string filterType, string searchedWord) 
        {
            
            List<Post> filteredPosts = new List<Post>();

            if (filterType== "Description")
            {
                dbrContext.posts.Include(p => p.Restaurant).ToList().ForEach(delegate (Post p) {

                    if (p.Description.ToLower().Contains(searchedWord.ToLower().ToString()))
                    {
                        filteredPosts.Add(p);
                    }

                });
            }
            else
            {
                dbrContext.posts.Include(p => p.Restaurant).ToList().ForEach(delegate (Post p) {
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
