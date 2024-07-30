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
        public DbSet<DiningTable> DiningTable { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<RestaurantBranch> RestaurantBranches { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        public DbSet<User> User { get; set; }

       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     OnModelCreatingPartial(modelBuilder);
       // }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
