using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restaurant.Data;
using restaurant.Models;

namespace restaurant.Services
{
    public class RestaurantService
    {
        private DBRContext dbrContext;
        public RestaurantService(DBRContext dbrContext)
        {
            this.dbrContext = dbrContext;
        }

        public void Create(Restaurant restaurant)
        {
            dbrContext.restaurants.Add(restaurant);
            dbrContext.SaveChanges();
        }

        public void Delete(int id)
        {
            dbrContext.restaurants.Remove(GetById(id));
            dbrContext.SaveChanges();
        }

        public void Edit(Restaurant restaurant)
        {
            Restaurant oldRestaurant = GetById(restaurant.Id);
            oldRestaurant.Address = restaurant.Address;
            oldRestaurant.Phone = restaurant.Phone;
            oldRestaurant.Type = restaurant.Type;
            oldRestaurant.WorkHours = restaurant.WorkHours;
            oldRestaurant.Menu = restaurant.Menu;
            dbrContext.SaveChanges();
        }

        public void AddEmptySeats(int id, int vacantSeats)
        {
            Restaurant oldRestaurant = GetById(id);
            oldRestaurant.EmptySeats += vacantSeats;

            dbrContext.SaveChanges();
        }

        public void RemoveEmptySeats(int id, int takenSeats)
        {
            Restaurant oldRestaurant = GetById(id);
            oldRestaurant.EmptySeats -= takenSeats;

            dbrContext.SaveChanges();
        }

        public Restaurant GetById(int id)
        {
            return dbrContext.restaurants.FirstOrDefault(p => p.Id == id);
        }
    }
}
