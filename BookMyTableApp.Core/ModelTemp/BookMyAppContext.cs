using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookMyTableApp.Core.ModelTemp;

public partial class BookMyAppContext : DbContext
{
    public BookMyAppContext()
    {
    }

    public BookMyAppContext(DbContextOptions<BookMyAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiningTable> DiningTables { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<RestaurantBranch> RestaurantBranches { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-9O6V9AD\\SQLEXPRESS;Initial Catalog=BookMyApp;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
