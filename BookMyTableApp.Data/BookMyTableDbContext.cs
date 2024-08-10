using BookMyTableApp.Core.ModelTemp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookMyTableApp.Data
{
    public partial class BookMyTableDbContext:DbContext
    {
        public BookMyTableDbContext(DbContextOptions<BookMyTableDbContext> options):base(options)
        {}

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<DiningTable> DiningTables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RestaurantBranch> RestaurantBranches { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<User> Users { get; set; }

       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     OnModelCreatingPartial(modelBuilder);
       // }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
