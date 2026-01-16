using System;
using System.ComponentModel.DataAnnotations;

namespace MountainRoutes.Models
{
    public class Mountain
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        public double Height { get; set; }

        public DateTime FirstAscent { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
