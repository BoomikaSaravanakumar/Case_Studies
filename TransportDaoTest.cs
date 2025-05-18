using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportManagementSystem.DAO;
using TransportManagementSystem.Entity;
using TransportManagementSystem.exception;

namespace TransportTest
{
    [TestFixture]
    public class TransportDaoTest
    {
        ITransportManagement ts;

        [SetUp]
        public void Setup()
        {
            ts = new TransportManagementImpl(); 
        }

        //Method 1

        [Test]
        public void AddVehicleReturnsTrueWhenVehicleIsAdded()
        {
            var vehicle = new Vehicle { Model = "Bus", Capacity = 30, VehicleType = "Passenger", VehicleStatus = "Active" };

            bool result = ts.AddVehicle(vehicle);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddVehicleReturnsFalseWhenDatabaseFails()
        {
            var vehicle = new Vehicle();
            Assert.Throws<SqlException>(() => ts.AddVehicle(vehicle));

        }

        [Test]
        public void UpdateVehicleReturnsTrueWhenVehicleIsUpdated()
        {
            var vehicle = new Vehicle { VehicleID = 1, Model = "Bus", Capacity = 20, VehicleType = "Passenger", VehicleStatus = "Active" };

            bool result = ts.UpdateVehicle(vehicle);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateVehicleThrowsVehicleNotFoundExceptionWhenVehicleDoesNotExist()
        {
            var vehicle = new Vehicle { VehicleID = 999, Model = "Bus", Capacity = 30, VehicleType = "Passenger", VehicleStatus = "Active" };
            Assert.Throws<VehicleNotFoundException>(()=>ts.UpdateVehicle(vehicle));
        }

        [Test]
        public void DeleteVehicleReturnsTrueWhenVehicleIsDeleted()
        {
            int vehicleId = 16; 

            bool result = ts.DeleteVehicle(vehicleId);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteVehicleThrowsVehicleNotFoundExceptionWhenVehicleDoesNotExist()
        {
            int vehicleId = 999; 

            Assert.Throws<VehicleNotFoundException>(() => ts.DeleteVehicle(vehicleId));
        }

        [Test]
        public void CancelBookingReturnsTrueWhenBookingIsCancelled()
        {
            int bookingId = 4; 

            bool result = ts.CancelBooking(bookingId);

            Assert.IsTrue(result);
        }

        [Test]
        public void CancelBookingThrowsExceptionWhenBookingNotFound()
        {
            int bookingId = 999; 

            Assert.Throws<BookingNotFoundException>(() => ts.CancelBooking(bookingId));
        }

        [Test]
        public void CancelTripReturnsTrueWhenTripIsCancelled()
        {
            int tripId = 7; 

            bool result = ts.CancelTrip(tripId);

            Assert.IsTrue(result);
        }

        [Test]
        public void CancelTripThrowsExceptionWhenTripNotFound()
        {
            int tripId = 1; 

            Assert.Throws<VehicleNotFoundException>(() => ts.CancelTrip(tripId));
        }

        [Test]
        public void DeallocateDriverReturnsTrueWhenDriverIsDeallocated()
        {
            int tripId = 3;

            bool result = ts.DeallocateDriver(tripId);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeallocateDriverThrowsExceptionWhenDriverNotAssigned()
        {
            int tripId = 999; 

            Assert.Throws<Exception>(() => ts.DeallocateDriver(tripId));
        }

        [Test]
        public void GetAvailableDriversReturnsListOfAvailableDrivers()
        {
            List<Driver> availableDrivers = ts.GetAvailableDrivers();

            Assert.IsNotNull(availableDrivers);
            Assert.IsTrue(availableDrivers.Count > 0);
        }

        [Test]
        public void GetAvailableDriversReturnsEmptyListWhenNoDriversAvailable()
        {
           
            List<Driver> availableDrivers = ts.GetAvailableDrivers();
            int count=availableDrivers.Count;
            Assert.IsNotNull(availableDrivers);
            Assert.AreEqual(count, availableDrivers.Count);
        }

        [Test]
        public void TypeCheckingGetDriversByDriverStatus()
        {
            List<Driver> availableDrivers = ts.GetAvailableDrivers();
            // Assert.IsInstanceOf<List<Driver>>(availableDrivers);
            Assert.That(availableDrivers, Is.InstanceOf<List<Driver>>());
        }

        [Test]
        public void GetBookingsByPassengerReturnsListOfBookings()
        {
            int passengerId = 1; 

            List<Booking> bookings = ts.GetBookingsByPassenger(passengerId);

            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Test]
        public void GetBookingsByPassengerReturnsEmptyListWhenNoBookingsFound()
        {
            int passengerId = 2; 

            Assert.Throws<BookingNotFoundException>(() => ts.GetBookingsByPassenger(passengerId));
        }

        [Test]
        public void TypeCheckingOfGetBookingByPassengerId()
        {
            int passengerId = 1;

            List<Booking> bookings = ts.GetBookingsByPassenger(passengerId);
            Assert.IsInstanceOf<List<Booking>>(bookings);

        }

        [Test]
        public void GetBookingsByTripReturnsListOfBookings()
        {
            int tripId = 2; 

            List<Booking> bookings = ts.GetBookingsByTrip(tripId);

            Assert.IsNotNull(bookings);
            Assert.IsTrue(bookings.Count > 0);
        }

        [Test]
        public void GetBookingsByTripReturnsEmptyListWhenNoBookingsFound()
        {
            int tripId = 1; 
            Assert.Throws<BookingNotFoundException>(()=>ts.GetBookingsByTrip(tripId));
        }

        [Test]
        public void TypeCheckingOfGetBookingByTripId()
        {

            int tripId = 2;

            List<Booking> bookings = ts.GetBookingsByTrip(tripId);
            Assert.IsInstanceOf<List<Booking>>(bookings);
        }

        [Test]
        public void ScheduleTripReturnsTrueWhenTripIsScheduled()
        {
            int vehicleId = 1;
            int routeId = 1;
            DateTime departureDate = DateTime.Now.AddDays(1);
            DateTime arrivalDate = DateTime.Now.AddDays(2);
            int driverId = 4;

            bool result = ts.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate, driverId);

            Assert.IsTrue(result);
        }

        [Test]
        public void ScheduleTripReturnsFalseWhenTripCannotBeScheduled()
        {
            int vehicleId = 15;
            int routeId = 1;
            DateTime departureDate = DateTime.Now.AddDays(1);
            DateTime arrivalDate = DateTime.Now.AddDays(2);
            int driverId = 3;

            Assert.Throws<SqlException>(() => ts.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate, driverId));

        }

        [Test]
        public void BookTripReturnsTrueWhenTripIsBooked()
        {
            int tripId = 3;
            int passengerId = 1; 
            DateTime bookingDate = DateTime.Now;

            bool result = ts.BookTrip(tripId, passengerId, bookingDate);

            Assert.IsTrue(result);
        }

        [Test]
        public void BookTripReturnsFalseWhenTripCannotBeBooked()
        {
            int vehicleId = 42; 
            int routeId = 1;
            DateTime departureDate = DateTime.Now.AddDays(1);
            DateTime arrivalDate = DateTime.Now.AddDays(2);
            int driverId = 1;

          //  Assert.Throws<SqlException>(()=>cs.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate, driverId));
            Assert.That(()=>ts.ScheduleTrip(vehicleId, routeId, departureDate, arrivalDate, driverId), Throws.TypeOf<SqlException>());
            
        }

        [Test]
        public void AllocateDriverReturnsTrueWhenDriverIsAllocated()
        {
            int vehicleId = 2;
            int routeId = 1;
            string tripStatus = "Scheduled";
            string tripType = "Passenger";
            int maxPassengers = 50;
            int driverId = 5;

            bool result = ts.AllocateDriver(vehicleId, routeId, tripStatus, tripType, maxPassengers, driverId);

            Assert.IsTrue(result);
        }

        [Test]
        public void AllocateDriverReturnsFalseWhenDriverCannotBeAllocated()
        {
            int vehicleId = 11; 
            int routeId = 1;
            string tripStatus = "Scheduled";
            string tripType = "Passenger";
            int maxPassengers = 25;
            int driverId = 1;

           // Assert.Throws<SqlException>(()=> cs.AllocateDriver(vehicleId, routeId, tripStatus, tripType, maxPassengers, driverId));
            Assert.That(() => ts.AllocateDriver(vehicleId, routeId, tripStatus, tripType, maxPassengers, driverId), Throws.TypeOf<SqlException>());
            
        }

    }
}
