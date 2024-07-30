using BookMyTableApp.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyTableApp.Data
{
    public interface IRestaurantRepository
    {
        Task<List<RestaurantModel>>GetAllRestaurantsAsync();
    }
}
