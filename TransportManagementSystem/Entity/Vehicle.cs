using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Entity
{
    public class Vehicle
    {
            public int VehicleID { get; set; }
            public string Model { get; set; }
            public decimal Capacity { get; set; }
            public string VehicleType { get; set; }
            public string VehicleStatus { get; set; }

            public Vehicle() { }

            public Vehicle(int vehicleID, string model, decimal capacity, string vehicle_Type, string vehicle_Status)
            {
                VehicleID = vehicleID;
                Model = model;
                Capacity = capacity;
                VehicleType = vehicle_Type;
                VehicleStatus = vehicle_Status;
            }
            public override string ToString()
            {
                return $"VehicleID: {VehicleID}, Model: {Model}, Capacity: {Capacity}, " +
                   $"Type: {VehicleType}, Status: {VehicleStatus}";
            }

    }
}

