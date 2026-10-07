using System;
using System.Collections.Generic;
using System.Text;
using static Part2_HotelReservationSystem.src.Reservation;

namespace Part2_HotelReservationSystem.src
{
    public class Room
    {
        public int RoomNumber { get; }
        public enum TypeRoom
        {
                Single,
                Double,
                Suite

        };
        public TypeRoom RoomType { get; }
        public decimal NightlyRate {  get;private  set;}
        public bool IsUnderMaintenance { get; private set; }
        private readonly List<Reservation> _reservations = new List<Reservation>();
        public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

        public Room(int roomNumber, TypeRoom roomType, decimal nightlyRate)
        {
            if (roomNumber <= 0)
                throw new ArgumentException("Room number must be greater than zero.", nameof(roomNumber));

            if (nightlyRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(nightlyRate), "Nightly rate must be a positive value.");

            RoomNumber = roomNumber;
            RoomType = roomType;
            NightlyRate = nightlyRate;
            IsUnderMaintenance = false;
        }

        public void UpdateNightlyRate(decimal newRate)
        {
            if (newRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(newRate), "Nightly rate must be a positive value.");

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


        public bool HasOverlappingReservation(DateTime checkIn, DateTime checkOut)
        {
            foreach (var reserve in _reservations)
            {

                if (reserve.Status == ReservationStatus.Cancelled || reserve.Status == ReservationStatus.CheckedOut)
                    continue;

                if (checkIn < reserve.CheckOutDate && checkOut > reserve.CheckInDate)
                    return true; 
            }

            return false; 
        }

        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }
    }

}

