using System;
namespace ParkingLot_Management
{
    class Program
    {
        static void Main()
        {
            ParkingLot lot = new ParkingLot(10);
            while (true)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("1. Add Vehicle to ParkingLot: ");
                Console.WriteLine("2. Remove vehicle from ParkingLot: ");
                Console.WriteLine("3. View vehicles in the ParkingLot: ");
                Console.WriteLine("4. Update Vehicle Number");
                Console.WriteLine("5. Exit: ");
                Console.WriteLine("--------------------------------------------");
                Console.Write("Enter choice(1/2/3/4/5): ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter the vehicle type (car/bike/van): ");
                            string typeInput = Console.ReadLine();

                            if (!Enum.TryParse(typeInput, true, out VehicleType type) ||
                                (type != VehicleType.Car && type != VehicleType.Bike && type != VehicleType.Van))
                            {
                                Console.WriteLine("Invalid vehicle type");
                                break;
                            }

                            Console.Write("Enter the vehicle number: ");
                            string number = Console.ReadLine().Trim();

                            Vehicle vehicle = type switch
                            {
                                VehicleType.Car => new Car(number),
                                VehicleType.Bike => new Bike(number),
                                VehicleType.Van => new Van(number),
                                _ => null
                            };

                            lot.ParkVehicle(vehicle);
                            break;

                        case 2:
                            Console.Write("Enter the vehicle number to remove: ");
                            string removeNumber = Console.ReadLine().Trim();
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }
    }
}
