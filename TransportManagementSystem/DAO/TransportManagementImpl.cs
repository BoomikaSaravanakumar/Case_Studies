using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TransportManagementSystem.Entity;
using TransportManagementSystem.exception;
using TransportManagementSystem.Util;


namespace TransportManagementSystem.DAO
{
    public class TransportManagementImpl:ITransportManagement
    {
        SqlConnection con = null;
        SqlCommand command = null;

        public bool AddVehicle(Vehicle vehicle)
        {   
            string query = "insert into Vehicles (model, capacity, vehicle_Type, vehicle_Status) values (@vmodel, @vcapacity, @vehicle_Type, @vehicle_Status)";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {

                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@vmodel", vehicle.Model);
                    command.Parameters.AddWithValue("@vcapacity", vehicle.Capacity);
                    command.Parameters.AddWithValue("@vehicle_Type", vehicle.VehicleType);
                    command.Parameters.AddWithValue("@vehicle_Status", vehicle.VehicleStatus);
                    result = command.ExecuteNonQuery();
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldnot Add Vehicle");
            }

            return result>0;
        }

        //--------------------------------------------------------------------------------------------------------------------
        public bool UpdateVehicle(Vehicle vehicle)
        {

            string query = "update Vehicles set model = @vmodel, capacity = @vcapacity, vehicle_type = @vehicle_Type, vehicle_status = @vehicle_Status WHERE VehicleID = @vehicleID";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);

                    command.Parameters.AddWithValue("@vmodel", vehicle.Model);
                    command.Parameters.AddWithValue("@vcapacity", vehicle.Capacity);
                    command.Parameters.AddWithValue("@vehicle_Type", vehicle.VehicleType);
                    command.Parameters.AddWithValue("@vehicle_Status", vehicle.VehicleStatus);
                    command.Parameters.AddWithValue("@vehicleID", vehicle.VehicleID);
                    result = command.ExecuteNonQuery();
                   
                }
                if (result == 0)
                {
                    throw new VehicleNotFoundException("Vehicle not found with ID: " + vehicle.VehicleID);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (VehicleNotFoundException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not Update");
            }

            return result>0;
        }

        //----------------------------------------------------------------------------------------------------------------

        public bool DeleteVehicle(int vehicleId)
        {
            string query = "delete from Vehicles where VehicleID = @vehicleID";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@vehicleID", vehicleId);
                    result = command.ExecuteNonQuery();  
                }
                if (result == 0)
                {
                    throw new VehicleNotFoundException("Vehicle not found with ID: " + vehicleId);
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            catch (VehicleNotFoundException ex)
            {

                throw ex;
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldnot Delete Vehicle");
            }

            return result>0;
        }

        //----------------------------------------------------------------------------------------------------------
        public bool CancelBooking(int bookingId)
        {
            string query = "update Bookings set bookingStatus = 'Cancelled' where BookingID = @bookingid";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@bookingid", bookingId);
                    result = command.ExecuteNonQuery();
                }
                if (result == 0)
                {
                    throw new BookingNotFoundException($"Booking not found with {bookingId}");
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            catch(BookingNotFoundException b)
            {
                throw b;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not Cancel the Booking");
            }
            return result > 0;
        }

        //-------------------------------------------------------------------------------------------------------------------

        public bool CancelTrip(int tripId)
        {
            string query = "update Trips set tripStatus = 'Cancelled' where TripID = @tripID";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@tripID", tripId);
                    result = command.ExecuteNonQuery();
                }
                if (result == 0)
                {
                    throw new VehicleNotFoundException("TripId with Vehicle id not found" );
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            catch(VehicleNotFoundException v)
            {
                throw v;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not Cancel the Trip");
            }
            return result > 0;
        }

        //----------------------------------------------------------------------------------------------------------------------

        public bool DeallocateDriver(int tripId)
        {
            string query = "update Trips set DriverID = Null where TripID = @tripID";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@tripID", tripId);
                    result = command.ExecuteNonQuery();
                }
                if(result == 0)
                {
                    throw new Exception("Could not Deallocate the Driver");
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }
            return result > 0;
        }

//---------------------------------------------------------------------------------------------------------------------------

        public List<Driver> GetAvailableDrivers()
        {
            List<Driver> drivers = new List<Driver>();
            string query = @"select * from Drivers 
                     where Status='Available';";
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Driver driver = new Driver
                        {
                            DriverID = (int)reader["DriverID"],
                            FirstName = (string)reader["FirstName"],
                            LicenseNumber =(string) reader["LicenseNumber"]
                        };
                        drivers.Add(driver);
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not fetch the available drivers");
            }
            return drivers;
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------

        public List<Booking> GetBookingsByPassenger(int passengerId)
        {
            List<Booking> bookings = new List<Booking>();
            string query = "select * from Bookings where PassengerID = @PassengerID";
            try
            {
                using (con = DBConnUtil.GetConnection())
                {

                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@PassengerID", passengerId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TripID = (int)reader["TripID"],
                            PassengerID = (int)reader["PassengerID"],
                            BookingDate = (DateTime)reader["BookingDate"],
                            BookingStatus = reader["BookingStatus"].ToString()
                        };
                        bookings.Add(booking);
                    }
                }
                if (bookings.Count == 0)
                {
                    throw new BookingNotFoundException("Booking not Found");
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (BookingNotFoundException b)
            {
                throw b;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not fetch th ebookings by passenger id ");
            }
           
            return bookings;
        }

        //----------------------------------------------------------------------------------------------------------------------
        public List<Booking> GetBookingsByTrip(int tripId)
        {
            List<Booking> bookings = new List<Booking>();
            string query = "select * from Bookings where TripID = @tripID";
            try
            {
                using (con = DBConnUtil.GetConnection())
                {

                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@tripID", tripId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking booking = new Booking
                        {
                            BookingID = (int)reader["BookingID"],
                            TripID = (int)reader["TripID"],
                            PassengerID = (int)reader["PassengerID"],
                            BookingDate = (DateTime)reader["BookingDate"],
                            BookingStatus =(string) reader["bookingStatus"]
                        };
                        bookings.Add(booking);
                    }
                }
                if(bookings.Count == 0)
                {
                    throw new BookingNotFoundException("Booking not Found");
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (BookingNotFoundException b)
            {
                throw b;
            }
            catch ( Exception e )
            {
                Console.WriteLine("could fetch bookings by trip");
            }

        
            return bookings;
        }

        //--------------------------------------------------------------------------------------------------------------------------
        public bool ScheduleTrip(int vehicleId, int routeId, DateTime departureDate, DateTime arrivalDate,int driverId)
        {
            string query = @"insert into Trips (vehicleID, routeID, departureDate, arrivalDate, tripStatus, tripType, maxPassengers,driverID) 
                     values (@vehicleID, @routeID, @departureDate, @arrivalDate, 'Scheduled', 'Passenger', 50,@driverId)";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@vehicleID", vehicleId);
                    command.Parameters.AddWithValue("@routeID", routeId);
                    command.Parameters.AddWithValue("@departureDate", departureDate);
                    command.Parameters.AddWithValue("@arrivalDate", arrivalDate);
                    command.Parameters.AddWithValue("@driverID",driverId);
                    result = command.ExecuteNonQuery();
                }
                if (result == 0)
                {
                    throw new Exception("Could not Schedule the trip");
                }
            }
            catch( SqlException e )
            {
                throw e;
            }
            catch(Exception e )
            {
                throw e;
            }
            return result > 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
        public bool BookTrip(int tripId, int passengerId, DateTime bookingDate)
        {
            string query = @"insert into Bookings (tripID, passengerID, bookingDate, bookingStatus) 
                     values (@tripID, @passengerID, @bookingDate, 'Confirmed')";
            int result = 0;

            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@tripID", tripId);
                    command.Parameters.AddWithValue("@passengerID", passengerId);
                    command.Parameters.AddWithValue("@bookingDate", bookingDate);
                    result = command.ExecuteNonQuery();
                }
                if(result == 0)
                {
                    throw new Exception("Could not Book the trip");
                }
            }
            catch ( SqlException e )
            {
                throw e;
            }
            catch(Exception e )
            {
                throw e;
            }

            return result > 0;
        }

        //-----------------------------------------------------------------------------------------------------------------------

        public bool AllocateDriver(int vehicleId,int routeId, string tripStatus, string tripType, int maxPassengers, int driverId)
        {
            string query = "insert into Trips (vehicleId,routeId,tripStatus,tripType,maxPassengers,driverID) values (@vehicleId,@routeId,@tripStatus,@tripType,@maxPassengers,@driverID)";
            int result = 0;
            try
            {
                using (con = DBConnUtil.GetConnection())
                {
                    command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@vehicleId", vehicleId);
                    command.Parameters.AddWithValue("@routeId", routeId);
                    command.Parameters.AddWithValue("@tripStatus", tripStatus);
                    command.Parameters.AddWithValue("@tripType", tripType);
                    command.Parameters.AddWithValue("@maxPassengers", maxPassengers);
                    command.Parameters.AddWithValue("@driverID", driverId);
                    result = command.ExecuteNonQuery();
                }
            }
            catch( SqlException e )
            {
                throw e;
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not allocate driver");
            }

            return result > 0;
        }

    }
}
