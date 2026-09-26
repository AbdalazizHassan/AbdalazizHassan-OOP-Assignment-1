using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2_HotelReservationSystem
{
    public class Guest
    {
        private readonly List<Reservation> _reservations = new();

        public int GuestId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

        public Guest(int guestId, string fullName, string phoneNumber)
        {
            if (guestId <= 0)
                throw new ArgumentException("Guest ID must be positive.", nameof(guestId));

            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Guest FullName must not be null or empty.", nameof(fullName));

            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Guest PhoneNumber must not be null or empty.", nameof(phoneNumber));

            GuestId = guestId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        internal void AddReservationInternal(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            _reservations.Add(reservation);
        }
    }
}
