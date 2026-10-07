using System;
using System.Collections.Generic;
using System.Text;

namespace Part2_HotelReservationSystem.src
{
        public class Guest
        {
            public int GuestId { get; }
            public string FullName { get; }
            public string PhoneNumber { get; }

            private readonly List<Reservation> _reservations = new List<Reservation>();
            public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

            public Guest(int guestId, string fullName, string phoneNumber)
            {
                if (guestId <= 0)
                    throw new ArgumentException("Guest ID must be greater than zero.", nameof(guestId));

                if (string.IsNullOrWhiteSpace(fullName))
                    throw new ArgumentException("Full name cannot be null or empty.", nameof(fullName));

                if (string.IsNullOrWhiteSpace(phoneNumber))
                    throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

                GuestId = guestId;
                FullName = fullName;
                PhoneNumber = phoneNumber;
            }

            public void MakeReservation(Reservation reservation)
            {
                if (reservation == null)
                    throw new ArgumentNullException(nameof(reservation), "Reservation cannot be null.");
               _reservations.Add(reservation);

            }
        }
}
