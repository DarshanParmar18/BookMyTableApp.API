using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyTableApp.Core.ModelTemp;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(128)]
    public string? AdObjId { get; set; }

    [StringLength(512)]
    public string? ProfileImageUrl { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public DateTime UpdatedDate { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
