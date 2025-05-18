using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem
{
    internal class UserInterface
    {
        public int GetVehicleID()
        {
            Console.WriteLine("Enter the Vehicle Id:");
            return int.Parse(Console.ReadLine());
        }
        public string GetModel()
        {
            Console.WriteLine("Enter the Model of Vehicle:");
            return Console.ReadLine();
        }
        public decimal GetCapacity()
        {
            Console.WriteLine("Enter the Vehicle Capacity:");
            return decimal.Parse(Console.ReadLine());
        }
        public string GetVehicleType()
        {
            Console.WriteLine("Enter the Vehicle Type:");
            return Console.ReadLine();
        }
        public string GetVehicleStatus()
        {
            Console.WriteLine("Enter the Vehicle Status:");
            return Console.ReadLine();
        }
        public int GetBookingId()
        {
            Console.WriteLine("Enter the BookingId:");
            return int.Parse(Console.ReadLine());

        }
        public int GetTripId()
        {
            Console.WriteLine("Enter the TripId:");
            return int.Parse(Console.ReadLine());

        }
        public int GetPassengerId()
        {
            Console.WriteLine("Enter the PassengerID:");
            return int.Parse(Console.ReadLine());

        }
        public int GetRouteId()
        {
            Console.WriteLine("Enter the RouteId:");
            return int.Parse(Console.ReadLine());
        }
        public DateTime GetArrivalDate()
        {
            Console.WriteLine("Enter the Arrival Date (yyyy-MM-dd HH:mm):");
            return DateTime.Parse(Console.ReadLine());
        }
        public DateTime GetDepartureDate()
        {
            Console.WriteLine("Enter the Departure Date (yyyy-MM-dd HH:mm):");
            return DateTime.Parse(Console.ReadLine());
        }
        public int GetDriverId()
        {
            Console.WriteLine("Enter the DriverID:");
            return int.Parse(Console.ReadLine());
        }
        public DateTime GetBookingDate()
        {
            Console.WriteLine("Enter the Booking Date (yyyy-MM-dd HH:mm):");
            return DateTime.Parse(Console.ReadLine());
        }
        public int GetMaxPassengers()
        {
            Console.WriteLine("Enter the Maximum Passengers count:");
            return int.Parse(Console.ReadLine());
        }
        public string GetTripType()
        {
            Console.WriteLine("Enter the TripType:");
            return Console.ReadLine();
        }
        public string GetTripStatus()
        {
            Console.WriteLine("Enter the Trip Status:");
            return Console.ReadLine();
        }
    }
}
