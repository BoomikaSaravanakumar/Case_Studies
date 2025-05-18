using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Entity
{
    public class Booking
        {
            public int BookingID { get; set; }
            public int TripID { get; set; }
            public int PassengerID { get; set; }
            public DateTime BookingDate { get; set; }
            public string BookingStatus { get; set; }

            public Booking() { }

            public Booking(int bookingID, int tripID, int passengerID, DateTime bookingDate, string bookingStatus)
            {
                BookingID = bookingID;
                TripID = tripID;
                PassengerID = passengerID;
                BookingDate = bookingDate;
                BookingStatus = bookingStatus;
            }
            public override string ToString()
            {
               return $"BookindId:{BookingID} Tripid:{TripID} Passengerid:{PassengerID} BookingDate:{BookingDate} Bookingstatus:{BookingStatus}";
            }
    }
    }

