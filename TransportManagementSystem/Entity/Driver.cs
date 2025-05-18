using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagementSystem.Entity
{
    public class Driver
    {
            public int DriverID { get; set; }              
            public string FirstName { get; set; }          
            public string LastName { get; set; }           
            public string LicenseNumber { get; set; }      
            public string PhoneNumber { get; set; }        
            public int TripID { get; set; }               
            public string Status { get; set; }

        public Driver()
        {
            
        }

        public Driver(int driverID, string firstName, string lastName, string licenseNumber, string phoneNumber, int tripID, string status)
        {
            DriverID = driverID;
            FirstName = firstName;
            LastName = lastName;
            LicenseNumber = licenseNumber;
            PhoneNumber = phoneNumber;
            TripID = tripID;
            Status = status;
        }

        public override string ToString()
        {
            return $"DriverID: {DriverID}, Name: {FirstName}, License: {LicenseNumber}";
        }

    }

}

