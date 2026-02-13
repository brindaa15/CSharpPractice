using System;
namespace ParkingLot_Management
{
    // ENUM – avoids hard-coded strings
    enum VehicleType
    {
        Car,
        Bike,
        Van
    }
    // STRUCT – immutable
    struct ParkingInfo
    {
        public DateTime EntryTime { get; }

        public ParkingInfo(DateTime time)
        {
            EntryTime = time;
        }
    }
        // Abstract class (Vehicle is a base class)
    abstract class Vehicle : ICalculateParkingFee
    {
        //Encapsulation
        protected string vehicleNumber;
        protected ParkingInfo parkingInfo;

        protected Vehicle(string number)
        {
            vehicleNumber = number.Trim().ToUpper();   // string methods
            parkingInfo = new ParkingInfo(DateTime.Now);
        }
        public string VehicleNumber => vehicleNumber;
        public DateTime EntryTime => parkingInfo.EntryTime;

        //Polymorphism
        public abstract VehicleType Type { get; }
        public abstract double CalculateFees();

        //Method
        public void UpdateVehicleNumber(string newNumber)
        {
            vehicleNumber = newNumber.Trim().ToUpper();
        }

        
    }
}
