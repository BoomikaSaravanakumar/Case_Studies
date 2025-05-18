using TransportManagementSystem.Entity;
using TransportManagementSystem.exception;
using TransportManagementSystem.DAO;


namespace TransportManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
                TransportManagementImpl service = new TransportManagementImpl();
                UserInterface ui = new UserInterface();

                while (true)
                {
                    Console.WriteLine("\n----------------------TRANSPORT MANAGEMENT SYSTEM-------------------------");
                    Console.WriteLine("1. Add Vehicle");
                    Console.WriteLine("2. Update Vehicle");
                    Console.WriteLine("3. Delete Vehicle");
                    Console.WriteLine("4. Schedule Trip");
                    Console.WriteLine("5. Cancel Trip");
                    Console.WriteLine("6. Book Trip");
                    Console.WriteLine("7. Cancel Booking");
                    Console.WriteLine("8. Allocate Driver");
                    Console.WriteLine("9. Deallocate Driver");
                    Console.WriteLine("10. Get Bookings By Passenger");
                    Console.WriteLine("11. Get Bookings By Trip");
                    Console.WriteLine("12. Get Available Drivers");
                    Console.WriteLine("13. Exit");
                    Console.Write("Enter your choice: ");

                    int choice = int.Parse(Console.ReadLine());

                    try
                    {
                        switch (choice)
                        {
                            case 1:

                                string model = ui.GetModel();                                
                                decimal capacity = ui.GetCapacity();                               
                                string vehicle_Type = ui.GetVehicleType();                               
                                string vehicle_Status = ui.GetVehicleStatus();
                                Vehicle newVehicle = new Vehicle(0, model, capacity, vehicle_Type, vehicle_Status);
                                Console.WriteLine(service.AddVehicle(newVehicle) ? "Vehicle Added." : "Failed to add vehicle.");
                                break;

                            case 2:
                                
                                int updateId = ui.GetVehicleID();                               
                                string newModel = ui.GetModel();
                                decimal newCapacity = ui.GetCapacity();                               
                                string newType = ui.GetVehicleType();
                                string newStatus = ui.GetVehicleStatus();
                                Vehicle updatedVehicle = new Vehicle(updateId, newModel, newCapacity, newType, newStatus);
                                Console.WriteLine(service.UpdateVehicle(updatedVehicle) ? "Vehicle Updated." : "Failed to update vehicle.");
                                break;

                            case 3:
                                
                                int deleteId = ui.GetVehicleID();
                                Console.WriteLine(service.DeleteVehicle(deleteId) ? "Vehicle Deleted." : "Failed to delete vehicle.");
                                break;

                            case 4:

                                int vehicleId = ui.GetVehicleID();                               
                                int routeId = ui.GetRouteId();                               
                                DateTime departure = ui.GetDepartureDate();                               
                                DateTime arrival = ui.GetArrivalDate();                               
                                int driversId=ui.GetDriverId();
                                Console.WriteLine(service.ScheduleTrip(vehicleId, routeId, departure, arrival,driversId) ? "Trip Scheduled." : "Failed to schedule trip.");
                                break;

                            case 5:
                                
                                int cancelTrip = ui.GetTripId();
                                Console.WriteLine(service.CancelTrip(cancelTrip) ? "Trip Cancelled." : "Failed to cancel trip.");
                                break;

                            case 6:

                                int bookTrip = ui.GetTripId();                               
                                int passengerId = ui.GetPassengerId();                              
                                DateTime bookingDate = ui.GetBookingDate();
                                Console.WriteLine(service.BookTrip(bookTrip, passengerId, bookingDate) ? "Trip Booked." : "Failed to book trip.");
                                break;

                            case 7:
                                
                                int bookingId =ui.GetBookingId();
                                Console.WriteLine(service.CancelBooking(bookingId) ? "Booking Cancelled." : "Failed to cancel booking.");
                                break;

                            case 8:
                               
                                int vid= ui.GetVehicleID();                               
                                int rid= ui.GetRouteId();                             
                                string sts= ui.GetTripStatus();                               
                                string trtype= ui.GetTripType();                               
                                int count= ui.GetMaxPassengers();                              
                                int driverId = ui.GetDriverId();
                                Console.WriteLine(service.AllocateDriver(vid,rid,sts,trtype,count,driverId) ? "Driver Allocated." : "Failed to allocate driver.");
                                break;

                            case 9:

                                int deallocateTripId = ui.GetTripId();
                                Console.WriteLine(service.DeallocateDriver(deallocateTripId) ? "Driver Deallocated." : "Failed to deallocate driver.");
                                break;

                            case 10:
                                
                                int searchPassenger = ui.GetPassengerId();
                                List<Booking> bookingsByPassenger = service.GetBookingsByPassenger(searchPassenger);
                                foreach (var b in bookingsByPassenger)
                                {
                                    Console.WriteLine(b);
                                }
                                break;

                            case 11:
                                
                                int tripId = ui.GetTripId();
                                List<Booking> bookingsByTrip = service.GetBookingsByTrip(tripId);
                                foreach (var b in bookingsByTrip)
                                {
                                    Console.WriteLine(b);
                                }
                                break;

                            case 12:
                                List<Driver> drivers = service.GetAvailableDrivers();
                                foreach (var d in drivers)
                                {
                                    Console.WriteLine(d);
                                }
                                break;

                            case 13:
                                Console.WriteLine("Exiting... Thankyou!");
                                return;

                            default:
                                Console.WriteLine("Invalid choice. Try again.");
                                break;
                        }
                    }
                    catch (VehicleNotFoundException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    catch (BookingNotFoundException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unexpected Error: " + ex.Message);
                    }
                }
            }
        }
    }


