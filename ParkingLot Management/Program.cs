using System;
namespace ParkingLot_Management
{
    class Program
    {
        static void Main()
        {
            ParkingLot lot = new ParkingLot(10);
            //Loop
            while (true)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("1. Add Vehicle to ParkingLot: ");
                Console.WriteLine("2. Remove vehicle from ParkingLot: ");
                Console.WriteLine("3. View vehicles in the ParkingLot: ");
                Console.WriteLine("4. Update Vehicle Number");
                Console.WriteLine("5. Exit: ");
                Console.WriteLine("--------------------------------------------");
                Console.Write("Enter choice(1/2/3/4/5):");

                // Check whether the input is integer
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                //Conditional Statement
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the vehicle type(car/bike/van): ");
                        string type = Console.ReadLine().ToLower();
                        Vehicle vehicle = null;
                        if (type == "car" || type == "bike" || type == "van")
                        {
                            Console.WriteLine("Enter the vehicle number: ");
                            string number = Console.ReadLine().Trim();
                            if (type == "car")
                                vehicle = new Car(number);
                            else if (type == "bike")
                                vehicle = new Bike(number);
                            else if (type == "van")
                                vehicle = new Van(number);
                         lot.ParkVehicle(vehicle);
                        }
                        else
                        {
                            Console.WriteLine("Invalid vehicle type");
                            continue;
                        }
                        break;

                    case 2:
                        Console.WriteLine("Enter the vehicle number to remove: ");
                        string removeNumber = Console.ReadLine();
                        lot.RemoveVehicle(removeNumber);
                        break;

                    case 3:
                        lot.ViewParkedVehicles();
                        break;

                    case 4:
                        lot.UpdateVehicleFromConsole();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice...");
                        break;
                }
            }
        }
    }
}
