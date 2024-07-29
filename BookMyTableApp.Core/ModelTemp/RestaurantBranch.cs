using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookMyTableApp.Core.ModelTemp;

[Index("RestaurantId", Name = "IX_RestaurantBranches_RestaurantId")]
public partial class RestaurantBranch
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [InverseProperty("RestaurantBranch")]
    public virtual ICollection<DiningTable> DiningTables { get; set; } = new List<DiningTable>();

    [ForeignKey("RestaurantId")]
    [InverseProperty("RestaurantBranches")]
    public virtual Restaurant Restaurant { get; set; } = null!;
}
