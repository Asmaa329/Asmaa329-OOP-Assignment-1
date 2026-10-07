using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem.src
{
    public class Reservation
    {
        public int ReservationId { get; }
        public DateTime CheckInDate {  get;}
        public DateTime CheckOutDate { get;}
        public Room Room {  get; set;}

        public enum ReservationStatus
        {
            Pending,
            Confirmed,
            CheckedIn,
            CheckedOut,
            Cancelled
        }
        public ReservationStatus Status {  get;private  set;}

        public decimal TotalCost
        {
            get
            {
                int numberOfNights = (CheckOutDate - CheckInDate).Days;
                return numberOfNights * Room.NightlyRate;
            }
        }

        public Reservation(int reservationId, DateTime checkInDate, DateTime checkOutDate, Room room)
        {
            if (reservationId <= 0)
                throw new ArgumentException("Reservation ID must be positive.", nameof(reservationId));

            if (checkOutDate <= checkInDate)
                throw new ArgumentException("CheckOutDate must be strictly after CheckInDate.");

            if (room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");

            if (room.IsUnderMaintenance)
                throw new InvalidOperationException("Cannot create a reservation for a room currently under maintenance.");

            if (room.HasOverlappingReservation(checkInDate, checkOutDate))
                throw new InvalidOperationException("Room has an overlapping active reservation for the selected dates.");

            ReservationId = reservationId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Room = room;
            Status = ReservationStatus.Pending;

            room.AddReservation(this);
        }

        public void Confirm()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException($"Cannot confirm reservation. Current status is {Status}. Only Pending reservations can be confirmed.");

            Status = ReservationStatus.Confirmed;
        }

        public void CheckIn()
        {
            if (Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException($"Cannot check in. Reservation must be Confirmed first. Current status is {Status}.");

            Status = ReservationStatus.CheckedIn;
        }

        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException($"Cannot check out. Guest is not currently checked in. Current status is {Status}.");

            Status = ReservationStatus.CheckedOut;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending && Status != ReservationStatus.Confirmed)
                throw new InvalidOperationException($"Cannot cancel reservation. Only Pending or Confirmed reservations can be cancelled. Current status is {Status}.");

            Status = ReservationStatus.Cancelled;
        }


    }
}
