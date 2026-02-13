using System;

namespace ParkingLot_Management
{
    //Inheritance
    class Van : Vehicle
    {
        public Van(string number) : base(number) { }
        public override VehicleType Type => VehicleType.Van;
        //Runtime Polymorphism
        public override double CalculateFees()
        {
            double hours = (DateTime.Now - EntryTime).TotalHours;
            return Math.Round(hours * 30, 2);
        }
    }
}
