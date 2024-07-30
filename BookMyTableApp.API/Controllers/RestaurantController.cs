using BookMyTableApp.Core.ViewModel;
using BookMyTableApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookMyTableApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("restaurants")]
        [ProducesResponseType(200, Type =typeof(List<RestaurantModel>))]
        public async Task<ActionResult> GetAllRestaurantsAsync()
        {
            var restaurant = await _restaurantService.GetAllRestaurantsAsync();
            return Ok(restaurant);
        }

        [HttpGet("branches/{restaurantId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RestaurantBranchModel>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<RestaurantBranchModel>>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _restaurantService.GetRestaurantBranchesByRestaurantIdAsync(restaurantId);
            if (branches == null)
            {
                return NotFound();
            }
            return Ok(branches);
        }

        [HttpGet("dinningtables/{branchId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DinningTableWithTimeSlotsModel>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<DinningTableWithTimeSlotsModel>>> GetDinningTablesByBranchIdAsync(int branchId)
        {
            var dinningTables = await _restaurantService.GetDinningTablesByBranchIdAsync(branchId);
            if (dinningTables == null)
            {
                return NotFound();
            }
            return Ok(dinningTables);
        }

        [HttpGet("dinningtables/{branchId}/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DinningTableWithTimeSlotsModel>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<DinningTableWithTimeSlotsModel>>> GetDinningTablesByBranchIdAsync(int branchId, DateTime date)
        {
            var dinningTables = await _restaurantService.GetDinningTablesByBranchIdAsync(branchId,date);
            if (dinningTables == null)
            {
                return NotFound();
            }
            return Ok(dinningTables);
        }
    }
}
