using Cwk_Booking.Domain.Models;

namespace Cwk_Booking.Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
        }

        public List<Hotel> Hotels { get; set;}
        private List<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = 1,
                    Name = "Mufasa",
                    City = "Agbado",
                    Country = "Ghana",
                    Stars = 3,
                    Description = "Some beautiful location"
                },
                new Hotel
                {
                   HotelId = 2,
                    Name = "Lion King",
                    City = "Gorimapa",
                    Country = "NY",
                    Stars = 2,
                    Description = "Another beautiful location"
                }
            };
        }
    }
}
