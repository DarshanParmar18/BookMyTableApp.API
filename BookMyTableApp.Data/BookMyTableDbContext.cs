using BookMyTableApp.Core.ModelTemp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookMyTableApp.Data
{
    public partial class BookMyTableDbContext : DbContext
    {
        public BookMyTableDbContext(DbContextOptions<BookMyTableDbContext> options) : base(options)
        { }

        public virtual DbSet<DiningTable> DiningTables { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Restaurant> Restaurants { get; set; }

        public virtual DbSet<RestaurantBranch> RestaurantBranches { get; set; }

        public virtual DbSet<TimeSlot> TimeSlots { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}
