using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Entity
{
        public class Trip
        {
            public int TripID { get; set; }
            public int VehicleID { get; set; }
            public int RouteID { get; set; }
            public DateTime DepartureDate { get; set; }
            public DateTime ArrivalDate { get; set; }
            public string TripStatus { get; set; }
            public string TripType { get; set; } 
            public int MaxPassengers { get; set; }
            public int DriverID {  get; set; }

            public Trip() { }

            public Trip(int tripID, int vehicleID, int routeID, DateTime departure,
                DateTime arrival, string tripStatus, string tripType, int maxPassengers,int driverId)
            {
                TripID = tripID;
                VehicleID = vehicleID;
                RouteID = routeID;
                DepartureDate = departure;
                ArrivalDate = arrival;
                TripStatus = tripStatus;
                TripType = tripType;
                MaxPassengers = maxPassengers;
                DriverID = driverId;
            }
            public override string ToString()
            {
                return $"TripID: {TripID}, VehicleID: {VehicleID}, RouteID: {RouteID}, " +
                   $"Departure: {DepartureDate:yyyy-MM-dd HH:mm}, Arrival: {ArrivalDate:yyyy-MM-dd HH:mm}, " +
                   $"Status: {TripStatus}, Type: {TripType}, MaxPassengers: {MaxPassengers}, DriverID: {DriverID}";
            }

    }
}


