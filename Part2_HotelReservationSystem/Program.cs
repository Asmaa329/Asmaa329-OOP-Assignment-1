using Part2_HotelReservationSystem.src;
using static Part2_HotelReservationSystem.src.Room;

namespace Part2_HotelReservationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var room = new Room(101, TypeRoom.Single, 500m);
            var guest = new Guest(1, "Asmaa Diab", "+201000000000");


            DateTime checkIn = DateTime.Today.AddDays(1);
            DateTime checkOut = DateTime.Today.AddDays(4);
            var reservation = new Reservation(1001, checkIn, checkOut, room);
            guest.MakeReservation(reservation);


            Console.WriteLine($"Guest: {guest.FullName}");
            Console.WriteLine($"Room: {reservation.Room.RoomNumber}");
            Console.WriteLine($"Total Cost: {reservation.TotalCost} EGP");


            reservation.Confirm();
            Console.WriteLine($"Status: {reservation.Status}");
        }
    }
    
}
