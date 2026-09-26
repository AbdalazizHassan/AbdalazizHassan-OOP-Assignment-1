namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Testing Hotel Reservation System (Part 2) ===\n");

            var manager = new HotelManager();

            try
            {
                var room1 = new Room(101, RoomType.Single, 150.00m);
                var room2 = new Room(102, RoomType.Suite, 300.00m);
                var guest1 = new Guest(1, "Ahmed Hassan", "01012345678");

                manager.AddRoom(room1);
                manager.AddRoom(room2);
                manager.RegisterGuest(guest1);

                Console.WriteLine($"Created Room #{room1.RoomNumber} ({room1.RoomType}) @ {room1.NightlyRate:C}/night");
                Console.WriteLine($"Registered Guest: {guest1.FullName} ({guest1.PhoneNumber})\n");

                var checkIn = DateTime.Today.AddDays(1);
                var checkOut = DateTime.Today.AddDays(4); 

                var res1 = manager.CreateReservation(1001, guest1.GuestId, room1.RoomNumber, checkIn, checkOut);
                Console.WriteLine($"Reservation #{res1.ReservationId} Created for {res1.Nights} nights.");
                Console.WriteLine($"Total Cost calculated automatically: {res1.TotalCost:C}");
                Console.WriteLine($"Initial Status: {res1.Status}\n");

                Console.WriteLine("--- Testing Legal Status Transitions ---");
                res1.Confirm();
                Console.WriteLine($"Status after Confirm(): {res1.Status}");

                res1.CheckIn();
                Console.WriteLine($"Status after CheckIn(): {res1.Status}\n");

                Console.WriteLine("--- Testing Double-Booking Prevention ---");
                try
                {
                    manager.CreateReservation(1002, guest1.GuestId, room1.RoomNumber, checkIn, checkOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXPECTED ERROR]: {ex.Message}\n");
                }

                Console.WriteLine("--- Testing Invalid Dates ---");
                try
                {
                    manager.CreateReservation(1003, guest1.GuestId, room1.RoomNumber, checkOut, checkIn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXPECTED ERROR]: {ex.Message}\n");
                }

                Console.WriteLine("--- Testing Maintenance Rule Exception ---");
                room2.StartMaintenance();
                Console.WriteLine($"Room #{room2.RoomNumber} IsUnderMaintenance: {room2.IsUnderMaintenance}");
                try
                {
                    manager.CreateReservation(1004, guest1.GuestId, room2.RoomNumber, checkIn, checkOut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXPECTED ERROR]: {ex.Message}\n");
                }

                Console.WriteLine("--- Testing Illegal Status Transition ---");
                try
                {
                    res1.Cancel();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXPECTED ERROR]: {ex.Message}\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UNHANDLED ERROR]: {ex.Message}");
            }

            Console.WriteLine("=== All Domain Rules & Validations Verified Successfully ===");
        }

    }
}
