using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MountainRoutes.Models
{
    public class Hut
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        public int Capacity { get; set; }

        public bool HasRestaurant { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // Foreign key към Route
        [ForeignKey("Route")]
        public int RouteId { get; set; }
        public MountainRoute? Route { get; set; } // nullable navigation property
    }
}
