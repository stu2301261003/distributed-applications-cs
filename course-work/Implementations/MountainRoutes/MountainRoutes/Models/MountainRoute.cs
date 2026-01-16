using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountainRoutes.Models
{
    public class MountainRoute
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        public double LengthKm { get; set; }

        [MaxLength(50)]
        public string? Difficulty { get; set; } // Easy, Medium, Hard

        public DateTime CreatedAt { get; set; }

        // Foreign key към Mountain
        [ForeignKey("Mountain")]
        public int MountainId { get; set; }
        public Mountain? Mountain { get; set; } // nullable navigation property
    }
}
