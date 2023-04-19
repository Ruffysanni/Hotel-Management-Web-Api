using Cwk_Booking.Domain.Abstractions.Repositories;
using Cwk_Booking.Domain.Abstractions.Services;
using Cwk_Booking.Domain.Models;
using Cwk_Bookinng.Dal;
using Microsoft.EntityFrameworkCore;

namespace Cwk_Booking.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelrepo, DataContext ctx)
        {
            _hotelsRepository = hotelrepo;
            _ctx = ctx;
        }

        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {
            //Step 1: create a reservation instance
            //var reservation = new Reservation
            //{
            //    HotelId = hotelId,
            //    RoomId = roomId,
            //    CheckInDate = checkIn,
            //    CheckoutDate = checkOut,
            //    Customer = customer
            //};

            //Step 2: Get the hotel, including the lst of rooms
            var hotel = await _hotelsRepository.GetHotelsByIdAsync(reservation.HotelId);

            //Step 3: Find the specified room
            var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

            if(hotel == null || room == null)
            {
                return null;
            }

            //Step 4: Make sure the room is available
            //var isBusy = reservation.CheckInDate >= room.BusyFrom.Value && reservation.CheckoutDate <= room.BusyTo.Value;

            //if(isBusy && room.NeedRepair)
            //{
            //    return null;
            //}
            //var roomBusyFrom = room.BusyFrom == null ? default(DateTime?) : room.BusyFrom;
            //var roomBusyTo = room.BusyTo == null ? default(DateTime?) : room.BusyTo;
            //var isBusy = reservation.CheckInDate >= roomBusyFrom || reservation.CheckInDate <= roomBusyTo;

            bool isBusy = await _ctx.Reservations.AnyAsync(r =>
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckoutDate)
                && (reservation.CheckoutDate >= r.CheckInDate && reservation.CheckoutDate <= r.CheckoutDate)
            ) ;

            if (isBusy)
            {
                return null;
            }

            if (room.NeedRepair)
            {
                return null;
            }

            //Set BusyFrom and BusyTo on the room
            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckoutDate;

            // Step 6: Persist the hanges to the database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);

            await _ctx.SaveChangesAsync();

            return reservation;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var allReservation = await _ctx.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .ToListAsync();
            return allReservation;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reseervationId = await _ctx.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
            
            return reseervationId;
        }

        public async Task<Reservation> CancelReservationByAsync(int id)
        {
            var reservation = await _ctx.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if(reservation != null)
            {
                _ctx.Reservations.Remove(reservation);
            }

            await _ctx.SaveChangesAsync ();
            return reservation;
        }
    }
}
