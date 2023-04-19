using Cwk_Booking.Domain.Abstractions.Repositories;
using Cwk_Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cwk_Bookinng.Dal.Repositories
{
    public class HotelRepository : IHotelsRepository
    {
        private readonly DataContext _dataCtx;

        public HotelRepository(DataContext dataCtx)
        {
            _dataCtx = dataCtx;
        }
        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            _dataCtx.Hotels.Add(hotel);
            await _dataCtx.SaveChangesAsync();
            return hotel;
        }

        public async Task<Room> CreateHotelRoomAsync(int hotelId, Room room)
        {
            var hotel = await _dataCtx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.HotelId == hotelId);

            hotel.Rooms.Add(room);
            await _dataCtx.SaveChangesAsync();
            return room;
        }

        public async Task<Hotel> DeleteHotelAsync(int id)
        {
            var hotel = await _dataCtx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel== null)
                return null;

            _dataCtx.Hotels.Remove(hotel);
            await _dataCtx.SaveChangesAsync();
            return hotel;
        }

        public async Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId)
        {
            var room = await _dataCtx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);

            if (room == null)
                return null;

            _dataCtx.Rooms.Remove(room);
            await _dataCtx.SaveChangesAsync();
            return room;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _dataCtx.Hotels.ToListAsync();
        }

        public async Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId)
        {
            var room = await _dataCtx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);
            if (room == null)
                return null;
            return room;
        }

        public async Task<Hotel> GetHotelsByIdAsync(int id)
        {
            var hotel = await _dataCtx.Hotels
                                .Include(h => h.Rooms)
                                .FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return null;
            }
            return hotel;
        }

        public async Task<List<Room>> ListHotelRoomsAsync(int hotelId)
        {
            var rooms = await _dataCtx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
            return rooms;
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel updateHotel)
        {
            if (updateHotel == null)
            {
                return null;
            }

            //Saving to the model
            _dataCtx.Hotels.Update(updateHotel);
            await _dataCtx.SaveChangesAsync();
            return updateHotel;
        }

        public async Task<Room> UpdateHotelRoomAsync(int hotelId, Room updatedRoom)
        {
           // updatedRoom.HotelId = hotelId;
           // updatedRoom.RoomId = roomId;

            _dataCtx.Rooms.Update(updatedRoom);
            await _dataCtx.SaveChangesAsync();
            return updatedRoom;
        }
    }
}
