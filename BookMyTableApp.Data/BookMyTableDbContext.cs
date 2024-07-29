using BookMyTableApp.Core.ModelTemp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookMyTableApp.Data
{
    public partial class BookMyTableDbContext:DbContext
    {
        public BookMyTableDbContext(DbContextOptions<BookMyTableDbContext> options):base(options)
        {}

        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<DiningTable> DiningTable { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<RestaurantBranch> RestaurantBranch { get; set; }
        public DbSet<TimeSlot> TimeSlot { get; set; }
        public DbSet<User> User { get; set; }

       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     OnModelCreatingPartial(modelBuilder);
       // }
       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
