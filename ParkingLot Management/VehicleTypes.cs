using System;
namespace ParkingLot_Management
{
    //Single Inheritance
    class Car : Vehicle
    {
        public Car(string number) : base(number) { }
        public override string VehicleType => "Car";
        public override double CalculateFees() =>
            Math.Ceiling((DateTime.Now - EntryTime).TotalHours) * 25;
    }

    class Bike : Vehicle
    {
        public Bike(string number) : base(number) { }
        public override string VehicleType => "Bike";
        public override double CalculateFees() =>
            Math.Ceiling((DateTime.Now - EntryTime).TotalHours) * 15;
    }

    class Van : Vehicle
    {
        public Van(string number) : base(number) { }
        public override string VehicleType => "Van";
        public override double CalculateFees() =>
            Math.Ceiling((DateTime.Now - EntryTime).TotalHours) * 50;
    }
}
