using AutoMapper;
using Cwk_Booking.Api.DTOs;
using Cwk_Booking.Domain.Models;

namespace Cwk_Booking.Api.AutoMapper
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<HotelCreateDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();
        }
    }
}
