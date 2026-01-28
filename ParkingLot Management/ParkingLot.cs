using System;
using System.Collections.Generic;

namespace ParkingLot_Management
{
    class ParkingLot
    {
        private int maxCapacity;
        private List<Vehicle> ParkedVehicles;

        public ParkingLot(int capacity)
        {
            maxCapacity = capacity;
            ParkedVehicles = new List<Vehicle>();
        }
        //Park Vehicle
        public void ParkVehicle(Vehicle vehicle)
        {
            //checks if vehicle No already exist
            foreach (var v in ParkedVehicles)
            {
                if (v.VehicleNumber.Trim()==vehicle.VehicleNumber.Trim())
                {
                    Console.WriteLine("Error vehicle number already exists!");
                    return;
                }
            }
            if (ParkedVehicles.Count >= maxCapacity)
            {
                Console.WriteLine("Parking Full!!");
            }
            else
            {
                ParkedVehicles.Add(vehicle);
                Console.WriteLine($"{vehicle.VehicleType} parked. EntryTime:{vehicle.EntryTime}");
            }
        }
        //Remove Vehicle
        public void RemoveVehicle(string number)
        {
            Vehicle found = null;
            for (int i = 0; i < ParkedVehicles.Count; i++)
            {
                if (ParkedVehicles[i].VehicleNumber == number)
                {
                    found = ParkedVehicles[i];
                    ParkedVehicles.RemoveAt(i);
                    break;
                }
            }
            if (found != null)
            {
                Console.WriteLine($"Vehicle exited.Fees to be paid:${found.CalculateFees()}");
            }
            else
            {
                Console.WriteLine("Vehicle not found!");
            }
        }
        //View Parked Vehicle
        public void ViewParkedVehicles()
        {
            if (ParkedVehicles.Count == 0)
            {
                Console.WriteLine("No vehicles parked.");
                return;
            }
            Console.WriteLine("Parked vehicles:");
            
            foreach (Vehicle v in ParkedVehicles)
            {
                Console.WriteLine($"{v.VehicleNumber}({v.VehicleType})");
            }
        }
        //Update vehicle Number
        public void UpdateVehicle(string oldNumber, string newNumber)
        {
            Vehicle found = null;
            // To check if the new vehicle number is already present
            foreach (var v in ParkedVehicles)
            {
                if (v.VehicleNumber == newNumber)
                {
                    Console.WriteLine("Error New vehicle number already exists!");
                    return;
                }
            }
            for (int i = 0; i < ParkedVehicles.Count; i++)
            {
                if (ParkedVehicles[i].VehicleNumber == oldNumber)
                {
                    found = ParkedVehicles[i];
                    break;
                }
            }
            if (found != null)
            {
                found.UpdateVehicleNumber(newNumber);
                Console.WriteLine("Vehicle number updated successfully.");
            }
            else
            {
                Console.WriteLine("Vehicle not found!");
            }
        }
    }
}
