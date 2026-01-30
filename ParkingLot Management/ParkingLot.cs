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
        //Adds Vehicle in parking lot
        public void ParkVehicle(Vehicle vehicle)
        {
            //checks if vehicle Number already exist
            foreach (var v in ParkedVehicles)
            {
                if (v.VehicleNumber == vehicle.VehicleNumber)
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

        //Remove Vehicle and calculate parking fee
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

        //View  all Parked Vehicle
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

        //checks if parking lot is empty
        public bool IsEmpty()
        {
            return ParkedVehicles.Count == 0;
        }

        public void UpdateVehicleFromConsole()
        {
            if (IsEmpty())
            {
                Console.WriteLine("No vehicles parked.");
                return;
            }

            Console.Write("Enter the old vehicle number: ");
            string oldNumber = Console.ReadLine();

            Console.Write("Enter the new vehicle number: ");
            string newNumber = Console.ReadLine();

            UpdateVehicle(oldNumber, newNumber);
        }

        //Update vehicle Number
        public void UpdateVehicle(string oldNumber, string newNumber)
        {

            Vehicle found = null;
            // checks if old vehicle exists
            foreach (var v in ParkedVehicles)
            {
                if (v.VehicleNumber == oldNumber)
                {
                    found = v;
                    break;
                }
            }
            if (found == null)
            {
                Console.WriteLine("Vehicle not found");
                return;
            }
            //checks if new number vehicle is already parked
            foreach (var v in ParkedVehicles)
            {
                if (v.VehicleNumber == newNumber)
                {
                    Console.WriteLine("Error: New vehicle number already exists!");
                    return;
                }
            }
            //updates vehicle number
            found.UpdateVehicleNumber(newNumber);
            Console.WriteLine("Vehicle number updated successfully.");
        }
    }
}
