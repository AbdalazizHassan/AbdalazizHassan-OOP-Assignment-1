using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2_HotelReservationSystem
{
    public class Reservation
    {
        public int ReservationId { get; }
        public Guest Guest { get; }
        public Room Room { get; }
        public DateTime CheckInDate { get; }
        public DateTime CheckOutDate { get; }
        public ReservationStatus Status { get; private set; }

        public int Nights => (CheckOutDate - CheckInDate).Days;

        public decimal TotalCost => Nights * Room.NightlyRate;

        public Reservation(int reservationId, Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            if (reservationId <= 0)
                throw new ArgumentException("Reservation ID must be positive.", nameof(reservationId));

            if (guest == null)
                throw new ArgumentNullException(nameof(guest), "Guest cannot be null.");

            if (room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            if (checkOutDate <= checkInDate)
                throw new ArgumentException("CheckOutDate must be strictly after CheckInDate.", nameof(checkOutDate));

            if (room.IsUnderMaintenance)
                throw new InvalidOperationException($"Cannot create a reservation for Room #{room.RoomNumber} because it is under maintenance.");

            ReservationId = reservationId;
            Guest = guest;
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = ReservationStatus.Pending;
            
            guest.AddReservationInternal(this);
        }

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException($"Cannot confirm reservation from state: {Status}. Must be Pending.");

            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException($"Cannot check-in without confirmation. Current status is: {Status}.");

            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException($"Cannot check-out from state: {Status}. Reservation must be CheckedIn first.");

            Status = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException($"Cannot cancel a reservation from state: {Status}. Only Pending or Confirmed reservations can be cancelled.");

            Status = ReservationStatus.Cancelled;
        }

        public bool OverlapsWith(DateTime checkIn, DateTime checkOut)
        {
            if (Status == ReservationStatus.Cancelled || Status == ReservationStatus.CheckedOut)
                return false;

            return CheckInDate < checkOut && checkIn < CheckOutDate;
        }
    }
}
