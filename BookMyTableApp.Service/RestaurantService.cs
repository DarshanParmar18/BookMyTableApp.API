using BookMyTableApp.Core.ViewModel;
using BookMyTableApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableApp.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }
        public Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
             return restaurantRepository.GetAllRestaurantsAsync();
        }
    }
}
