using BookMyTableApp.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableApp.Data
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly BookMyTableDbContext _dbContext;

        public RestaurantRepository(BookMyTableDbContext dbContext )
        {
            _dbContext = dbContext;
        }
        public  Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
            var restaurant =  _dbContext.Restaurant
                .OrderBy( x => x.Name )
                .Select( r => new RestaurantModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    Phone = r.Phone,
                    Email = r.Email,
                    ImageUrl = r.ImageUrl,
                }).ToListAsync();
            return restaurant;
        }
    }
}
