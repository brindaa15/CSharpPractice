using System;

namespace ParkingLot_Management
{
    //Inheritance
    class Bike : Vehicle
    {
        public Bike(string number) : base(number) { }
        public override VehicleType Type => VehicleType.Bike;
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double hours = (DateTime.Now - EntryTime).TotalHours;
            return Math.Round(hours * 15, 2);
        }
    }
}
