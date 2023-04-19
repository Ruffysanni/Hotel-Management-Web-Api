using Cwk_Booking.Domain.Models;

namespace Cwk_Booking.Api.DTOs
{
    public class ReservationPostPutDto
    {
        
        public int RoomId { get; set; }
        public int HotelId { get; set; }
        public string Customer { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckoutDate { get; set; }
    }
}
