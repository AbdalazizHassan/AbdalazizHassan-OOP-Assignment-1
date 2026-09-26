using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2_HotelReservationSystem
{
    public class Room
    {
        public int RoomNumber { get; }
        public RoomType RoomType { get; }
        public decimal NightlyRate { get; private set; }
        public bool IsUnderMaintenance { get; private set; }

        public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
        {
            if (roomNumber <= 0)
                throw new ArgumentException("Room number must be greater than zero.", nameof(roomNumber));

            if (nightlyRate <= 0)
                throw new ArgumentException("Nightly rate must be a positive value.", nameof(nightlyRate));

            RoomNumber = roomNumber;
            RoomType = roomType;
            NightlyRate = nightlyRate;
            IsUnderMaintenance = false;
        }

        public void UpdateNightlyRate(decimal newRate)
        {
            if (newRate <= 0)
                throw new ArgumentException("Nightly rate must be a positive value.", nameof(newRate));

            NightlyRate = newRate;
        }

        public void StartMaintenance()
        {
            IsUnderMaintenance = true;
        }

        public void EndMaintenance()
        {
            IsUnderMaintenance = false;
        }
    }
}
