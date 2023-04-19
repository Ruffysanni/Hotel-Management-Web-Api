using AutoMapper;
using Cwk_Booking.Api.DTOs;
using Cwk_Booking.Domain.Abstractions.Repositories;
using Cwk_Booking.Domain.Models;
using Cwk_Bookinng.Dal;
using Cwk_Bookinng.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwk_Booking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepo;

        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository repo, IMapper mapper)
        {
            _hotelsRepo = repo;
            _mapper = mapper;

        }




        //Get
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelsRepo.GetAllHotelsAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);

            return Ok(hotelsGet);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelsRepo.GetHotelsByIdAsync(id);

            if(hotel == null)
            {
                return NotFound();
            }
            var hotelGet = _mapper.Map<HotelGetDto>(hotel);
            return  Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);
            await _hotelsRepo.CreateHotelAsync(domainHotel);

            //var domainHotel = new Hotel();
            //domainHotel.Name = hotel.Name;
            //domainHotel.Address = hotel.Address;
            //domainHotel.City = hotel.City;
            //domainHotel.Description = hotel.Description;
            //domainHotel.Country = hotel.Country;
            //domainHotel.Stars = hotel.Stars;

            

            //var hotelGetDto = new HotelGetDto();
            //hotelGetDto.Name = domainHotel.Name;
            //hotelGetDto.Stars = domainHotel.Stars;
            //hotelGetDto.Address = domainHotel.Address;
            //hotelGetDto.Description = domainHotel.Description;
            //hotelGetDto.City = domainHotel.City;
            //hotelGetDto.Country = domainHotel.Country;
            //hotelGetDto.HotelId = domainHotel.HotelId;

            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId}, hotelGet);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDto hotelUpdate, int id)
        {

            var updatedHotel = _mapper.Map<Hotel>(hotelUpdate);
            //var hotel = await _dataCtx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            updatedHotel.HotelId = id;
            //Properties to update in your model
            //hotel.Name = hotelUpdate.Name;
            //hotel.Description = hotelUpdate.Description;
            //hotel.Stars = hotelUpdate.Stars;
            if (updatedHotel == null)
            {
                return NotFound();
            }

            await _hotelsRepo.UpdateHotelAsync(updatedHotel);

            return NoContent();
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotelById(int id)
        {
            var hotel = await _hotelsRepo.DeleteHotelAsync(id);
            if(hotel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            var rooms = await _hotelsRepo.ListHotelRoomsAsync(hotelId);
            
            var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);
            return Ok(mappedRooms);
        }


        [HttpGet]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _hotelsRepo.GetHotelRoomByIdAsync(hotelId, roomId);
            
            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return Ok(mappedRoom);
        }

        [HttpPost]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom(int hotelId, [FromBody] RoomPostPutDto newRoom)
        {
            var room = _mapper.Map<Room>(newRoom);
            //room.HotelId = hotelId;

            //_dataCtx.Rooms.Add(room);
            //await _dataCtx.SaveChangesAsync();
            await _hotelsRepo.CreateHotelRoomAsync(hotelId, room);
            

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById), 
                                     new { hotelId = hotelId, roomId = mappedRoom.RoomId}, mappedRoom);
        }


        [HttpPut]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostPutDto updatedRoom)
        {
            var toUpdate = _mapper.Map<Room>(updatedRoom);
            toUpdate.HotelId = hotelId;
            toUpdate.RoomId = roomId;
            await _hotelsRepo.UpdateHotelRoomAsync(hotelId, toUpdate);

            return NoContent();
        }

        [HttpDelete]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoomFromHotel(int hotelId, int roomId)
        {
            var roomToDelete = await _hotelsRepo.DeleteHotelRoomAsync(hotelId, roomId);
            if (roomToDelete == null)
                return NotFound("Room not found.");
            return NoContent();
        }

    }
}
