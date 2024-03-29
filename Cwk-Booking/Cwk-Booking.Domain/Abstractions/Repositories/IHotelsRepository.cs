﻿using Cwk_Booking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cwk_Booking.Domain.Abstractions.Repositories
{
    public interface IHotelsRepository
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelsByIdAsync(int id);
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> UpdateHotelAsync(Hotel updateHotel);
        Task<Hotel> DeleteHotelAsync(int id);

        Task<List<Room>> ListHotelRoomsAsync(int hotelId);
        Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId);
        Task<Room> CreateHotelRoomAsync(int hotelId, Room room);
        Task<Room> UpdateHotelRoomAsync(int hotelId, Room updatedRoom);
        Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId);
    }
}
