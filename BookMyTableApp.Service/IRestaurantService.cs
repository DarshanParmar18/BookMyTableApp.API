using BookMyTableApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableApp.Service
{
    public interface IRestaurantService
    {
        Task<List<RestaurantModel>> GetAllRestaurantsAsync();

        Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId);

        Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchIdAsync(int branchId);

        Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchAndDateAsync(int branchId, DateTime date);

    }
}
