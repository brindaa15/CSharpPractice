using System;

namespace ParkingLot_Management
{
    // Abstract class (Vehicle is a base class)
     abstract class Vehicle:ICalculateParkingFee
    {
        //Fields
        private string vehicleNumber;
        private DateTime entryTime;

        //Parameterized constructor
        public Vehicle(string number)
        {
            vehicleNumber = number;
            entryTime = DateTime.Now;     // this gives the current time in system
        }
        //Encapsulation - Properties
        public string VehicleNumber => vehicleNumber;
        public DateTime EntryTime => entryTime;

        //Abstraction (should be implemented by derived classes)
        public abstract string VehicleType {  get; }
        public abstract double CalculateFees();

        //Method
        public void UpdateVehicleNumber(string newNumber)
        {
            vehicleNumber = newNumber.Trim();
        }
    }
}