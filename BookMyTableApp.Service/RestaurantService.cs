using BookMyTableApp.Core.ViewModel;
using BookMyTableApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookMyTableApp.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            this._restaurantRepository = restaurantRepository;
        }
        public Task<List<RestaurantModel>> GetAllRestaurantsAsync()
        {
             return _restaurantRepository.GetAllRestaurantsAsync();
        }

        public async Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            return await _restaurantRepository.GetRestaurantBranchesByRestaurantIdAsync(restaurantId);
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchIdAsync(int branchId, DateTime date)
        {
            return await _restaurantRepository.GetDinningTablesByBranchIdAsync(branchId,date);
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchIdAsync(int branchId)
        {
            return await _restaurantRepository.GetDinningTablesByBranchIdAsync(branchId);
        }

    }
}
