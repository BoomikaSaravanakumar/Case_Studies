using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagementSystem.Entity;

namespace TransportManagementSystem.DAO
{
    public interface ITransportManagement
        {
            bool AddVehicle(Vehicle vehicle);
            bool UpdateVehicle(Vehicle vehicle);
            bool DeleteVehicle(int vehicleId);

            bool ScheduleTrip(int vehicleId, int routeId, DateTime departureDate, DateTime arrivalDate,int driverId);
            bool CancelTrip(int tripId);

            bool BookTrip(int tripId, int passengerId, DateTime bookingDate);
            bool CancelBooking(int bookingId);

            bool AllocateDriver(int vehicleId, int routeId, string tripStatus, string tripType, int maxPassengers, int driverId);
            bool DeallocateDriver(int tripId);

            List<Booking> GetBookingsByPassenger(int passengerId);
            List<Booking> GetBookingsByTrip(int tripId);

            List<Driver> GetAvailableDrivers();
        }
    }

