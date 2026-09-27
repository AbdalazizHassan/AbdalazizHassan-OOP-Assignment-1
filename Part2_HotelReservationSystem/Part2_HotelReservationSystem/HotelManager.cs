using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2_HotelReservationSystem
{
    public class HotelManager
    {
        private readonly List<Room> _rooms = new();
        private readonly List<Guest> _guests = new();
        private readonly List<Reservation> _reservations = new();

        public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
        public IReadOnlyList<Guest> Guests => _guests.AsReadOnly();
        public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

        public void AddRoom(Room room)
        {
            if (room == null)
                throw new ArgumentNullException(nameof(room));

            if (_rooms.Any(r => r.RoomNumber == room.RoomNumber))
                throw new InvalidOperationException($"Room #{room.RoomNumber} already exists.");

            _rooms.Add(room);
        }

        public void RegisterGuest(Guest guest)
        {
            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            if (_guests.Any(g => g.GuestId == guest.GuestId))
                throw new InvalidOperationException($"Guest ID {guest.GuestId} already exists.");

            _guests.Add(guest);
        }

        public Reservation CreateReservation(int reservationId, int guestId, int roomNumber, DateTime checkIn, DateTime checkOut)
        {
            if (_reservations.Any(r => r.ReservationId == reservationId))
                throw new InvalidOperationException($"Reservation ID {reservationId} already exists.");

            var guest = _guests.FirstOrDefault(g => g.GuestId == guestId)
                ?? throw new KeyNotFoundException($"Guest with ID {guestId} was not found.");

            var room = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber)
                ?? throw new KeyNotFoundException($"Room #{roomNumber} was not found.");

            bool isDoubleBooked = _reservations.Any(r => r.Room.RoomNumber == roomNumber && r.OverlapsWith(checkIn, checkOut));
            if (isDoubleBooked)
                throw new InvalidOperationException($"Room #{roomNumber} is already booked for the requested dates.");

            var reservation = new Reservation(reservationId, guest, room, checkIn, checkOut);
            _reservations.Add(reservation);

            return reservation;
        }
    }
}
