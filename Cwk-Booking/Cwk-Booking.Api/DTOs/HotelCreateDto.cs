using Cwk_Booking.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Cwk_Booking.Api.DTOs
{
    public class HotelCreateDto
    {
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Range(1,2)]
        public int Stars { get; set; }

        [Required]
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
