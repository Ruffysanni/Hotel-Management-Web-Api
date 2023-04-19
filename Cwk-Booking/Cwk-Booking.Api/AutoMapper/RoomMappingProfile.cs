using AutoMapper;
using Cwk_Booking.Api.DTOs;
using Cwk_Booking.Domain.Models;

namespace Cwk_Booking.Api.AutoMapper
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<Room, RoomGetDto>();
            CreateMap<RoomPostPutDto, Room>();
        }
    }
}
