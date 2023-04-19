using AutoMapper;
using Cwk_Booking.Api.DTOs;
using Cwk_Booking.Domain.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Cwk_Booking.Api.AutoMapper
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<ReservationPostPutDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();
        }
    }
}
