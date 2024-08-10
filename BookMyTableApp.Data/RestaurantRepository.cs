using BookMyTableApp.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var restaurant =  _dbContext.Restaurants
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

        public async Task<IEnumerable<RestaurantBranchModel>> GetRestaurantBranchesByRestaurantIdAsync(int restaurantId)
        {
            var branches = await _dbContext.RestaurantBranches
                .Where(rb => rb.RestaurantId == restaurantId)
                .Select(rb => new RestaurantBranchModel
                {
                    Id = rb.Id,
                    RestaurantId = rb.RestaurantId,
                    Name = rb.Name,
                    Address = rb.Address,
                    Phone = rb.Phone,
                    Email = rb.Email,
                    ImageUrl = rb.ImageUrl
                }).ToListAsync();

            return branches;
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchIdAsync(int branchId)
        {
            // this is linq but not a lamda express, one of the initial way to querying
            var data = await(
                from rb in _dbContext.RestaurantBranches
                join dt in _dbContext.DiningTables on rb.Id equals dt.RestaurantBranchId
                join ts in _dbContext.TimeSlots on dt.Id equals ts.DiningTableId
                where dt.RestaurantBranchId == branchId // && ts.ReservationDay >= DateTime.Now.Date  --> Add this in future 
                orderby ts.Id, ts.MealType
                select new DinningTableWithTimeSlotsModel()
                {
                    BranchId = rb.Id,
                    Capacity = dt.Capacity,
                    TableName = dt.TableName,
                    MealType = ts.MealType,
                    ReservationDay = ts.ReservationDay,
                    TableStatus = ts.TableStatus,
                    TimeSlotId = ts.Id
                })
            .ToListAsync();

            return data;

            /* var dinningTables = await _dbContext.DiningTables
                 .Where(dt => dt.RestaurantBranchId == branchId)
                 .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
                 {
                     dt.RestaurantBranchId,
                     dt.TableName,
                     dt.Capacity,
                     ts.ReservationDay,
                     ts.MealType,
                     ts.TableStatus,
                     ts.Id
                 })
                 .OrderBy(ts => ts.Id)
                 .ThenBy(ts => ts.MealType)
                 .ToListAsync();

             return dinningTables.Select(dt => new DinningTableWithTimeSlotsModel
             {
                 BranchId = dt.RestaurantBranchId,
                 ReservationDay = dt.ReservationDay,
                 TableName = dt.TableName,
                 Capacity = dt.Capacity,
                 MealType = dt.MealType,
                 TableStatus = dt.TableStatus,
                 TimeSlotId = dt.Id

             });*/
        }

        public async Task<IEnumerable<DinningTableWithTimeSlotsModel>> GetDinningTablesByBranchAndDateAsync(int branchId, DateTime date)
        {

            var dinningTables = await _dbContext.DiningTables
                .Where(dt => dt.RestaurantBranchId == branchId)
                .SelectMany(dt => dt.TimeSlots, (dt, ts) => new
                {
                    dt.RestaurantBranchId,
                    dt.TableName,
                    dt.Capacity,
                    ts.ReservationDay,
                    ts.MealType,
                    ts.TableStatus,
                    ts.Id
                })
                .Where(ts => ts.ReservationDay.Date == date.Date)
                .OrderBy(ts => ts.Id)
                .ThenBy(ts => ts.MealType)
                .ToListAsync();

            return dinningTables.Select(dt => new DinningTableWithTimeSlotsModel
            {
                BranchId=dt.RestaurantBranchId ,
                ReservationDay=dt.ReservationDay ,
                TableName=dt.TableName ,
                Capacity=dt.Capacity ,
                MealType=dt.MealType ,
                TableStatus=dt.TableStatus ,
                TimeSlotId=dt.Id

            });
        }
   
    }
}
