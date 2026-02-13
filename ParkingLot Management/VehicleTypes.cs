using System;
namespace ParkingLot_Management
{
    //Hierarchical Inheritance
    class Car : Vehicle
    {
        public Car(string number) : base(number) { }
        public override string VehicleType => "Car";
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double minutes = (DateTime.Now - EntryTime).TotalMinutes;
            double fee = Math.Round((minutes / 60) * 25, 2);
            return fee;
        }
    }

    class Bike : Vehicle
    {
        public Bike(string number) : base(number) { }
        public override string VehicleType => "Bike";
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double minutes = (DateTime.Now - EntryTime).TotalMinutes;
            double fee = Math.Round((minutes / 60) * 15, 2);
            return fee;
        }       
    }

    class Van : Vehicle
    {
        public Van(string number) : base(number) { }
        public override string VehicleType => "Van";
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double minutes = (DateTime.Now - EntryTime).TotalMinutes;
            double fee = Math.Round((minutes / 60) * 30, 2);
            return fee;
        }
    }
}
