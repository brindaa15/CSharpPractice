using System;
namespace ParkingLot_Management
{
    class Program
    {
        static void Main()
        {
            ParkingLot lot = new ParkingLot(50);
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
                Console.Write("Enter choice:");

                int choice = int.Parse(Console.ReadLine());
                //Conditional Statement
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter the vehicle type(car/bike/van): ");
                        string type = Console.ReadLine();

                        Console.WriteLine("Enter the vehicle number: ");
                        string number = Console.ReadLine();
                        Vehicle vehicle = null;
                        if (type.ToLower() == "car")
                        {
                            vehicle = new Car(number);
                        }
                        else if (type.ToLower() == "bike")
                        {
                            vehicle = new Bike(number);
                        }
                        else if (type.ToLower() == "van")
                        {
                            vehicle = new Van(number);
                        }
                        else
                        {
                            Console.WriteLine("Invalid vehicle type");
                            continue;
                        }
                        lot.ParkVehicle(vehicle);
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
                        Console.Write("Enter the old vehicle number: ");
                        string oldNo = Console.ReadLine();
                        Console.Write("Enter the new vehicle number: ");
                        string newNo = Console.ReadLine();
                        lot.UpdateVehicle(oldNo, newNo);
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
