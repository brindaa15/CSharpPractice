using System;

namespace ParkingLot_Management
{
    class Car : Vehicle
    {
        //Inheritance
        public Car(string number) : base(number) { }
        public override VehicleType Type => VehicleType.Car;
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double hours = (DateTime.Now - EntryTime).TotalHours;
            return Math.Round(hours * 25, 2);
        }
    }
}
