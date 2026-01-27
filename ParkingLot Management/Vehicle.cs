using System;

namespace ParkingLot_Management
{
    class Vehicle
    {
        //Fields
        private string vehicleNumber;
        private DateTime entryTime;

        //Parameterized constructor
        public Vehicle(string number)
        {
            vehicleNumber = number;
            entryTime = DateTime.Now;       // this give the current time in system
        }
        //Encapsulation-Properties
        public string VehicleNumber => vehicleNumber;
        public DateTime EntryTime => entryTime;

        //Runtime Polymorphism
        public virtual string VehicleType => "Vehicle";
        public virtual double CalculateFees() => 0;

        //Method
        public void UpdateVehicleNumber(string newNumber)
        {
            vehicleNumber = newNumber.Trim();
        }
    }
}