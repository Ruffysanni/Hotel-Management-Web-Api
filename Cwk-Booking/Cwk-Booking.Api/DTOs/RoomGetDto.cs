using Cwk_Booking.Domain.Models;

namespace Cwk_Booking.Api.DTOs
{
    public class RoomGetDto
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedRepair { get; set; }
    }
}
