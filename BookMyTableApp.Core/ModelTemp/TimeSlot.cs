using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyTableApp.Core.ModelTemp;

[Index("DiningTableId", Name = "IX_TimeSlots_DiningTableId")]
public partial class TimeSlot
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DiningTableId { get; set; }

    [Required]
    public DateTime ReservationDay { get; set; }

    [Required]
    public string MealType { get; set; } = null!;

    [Required]
    public string TableStatus { get; set; } = null!;

    [ForeignKey("DiningTableId")]
    [InverseProperty("TimeSlots")]
    public virtual DiningTable DiningTable { get; set; } = null!;

    [InverseProperty("TimeSlot")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
