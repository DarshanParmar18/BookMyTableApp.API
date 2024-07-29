using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyTableApp.Core.ModelTemp;

[Index("TimeSlotId", Name = "IX_Reservations_TimeSlotId")]
[Index("UserId", Name = "IX_Reservations_UserId")]
public partial class Reservation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int TimeSlotId { get; set; }

    [Required]
    public DateTime ReservationDate { get; set; }

    [Required]
    public string ReservationStatus { get; set; } = null!;

    public bool ReminderSent { get; set; }

    [ForeignKey("TimeSlotId")]
    [InverseProperty("Reservations")]
    public virtual TimeSlot TimeSlot { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reservations")]
    public virtual User User { get; set; } = null!;
}
