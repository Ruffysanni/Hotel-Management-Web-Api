using AutoMapper;
using Cwk_Booking.Api.DTOs;
using Cwk_Booking.Domain.Abstractions.Services;
using Cwk_Booking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwk_Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPostPutDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var result = await _reservationService.MakeReservationAsync(reservation);

            if(result == null)
            {
                return BadRequest("Cannot make reservation");
            }

            var mappedReservation = _mapper.Map<ReservationGetDto>(result);

            return Ok(mappedReservation);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(mapped);
        }

        [HttpGet]
        [Route("reservationId")]
        public async Task<IActionResult> GetReservationById(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
            if(reservation == null)
            {
                return NotFound($"No reservation found for the id: {reservationId}");
            }

            var mapped = _mapper.Map<ReservationGetDto>(reservation);

            return Ok(mapped);
        }

        [HttpDelete]
        [Route("reservationId")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            var deletedreservation = await _reservationService.CancelReservationByAsync(reservationId);
            if (deletedreservation == null) return NotFound();
            return NoContent();
            
        }
    }
}
