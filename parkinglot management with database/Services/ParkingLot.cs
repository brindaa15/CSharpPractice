using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using ParkingLot_Management.Data;

namespace ParkingLot_Management
{
    class ParkingLot
    {
        private int maxCapacity;
        public ParkingLot(int capacity)
        {
            maxCapacity = capacity;
        }

        // Add vehicle using stored procedure (enforces maxCapacity)
        public void ParkVehicle(Vehicle vehicle)
        {
            using SqlConnection conn = DbConnection.Create();
            conn.Open();
            // Calling stored procedure: ParkVehicle
            using SqlCommand cmd = new SqlCommand("ParkVehicle", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Passing parameters
            cmd.Parameters.AddWithValue("@VehicleNumber", vehicle.VehicleNumber);
            cmd.Parameters.AddWithValue("@VehicleType", vehicle.Type.ToString());
            cmd.Parameters.AddWithValue("@EntryTime", vehicle.EntryTime);
            cmd.Parameters.AddWithValue("@MaxCapacity", maxCapacity);

            try
            {
                // ExecuteNonQuery is used for INSERT operations
                cmd.ExecuteNonQuery();
                Console.WriteLine($"{vehicle.Type} parked. EntryTime: {vehicle.EntryTime}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Remove vehicle using stored procedure
        public void RemoveVehicle(string number)
        {
            using SqlConnection conn = DbConnection.Create();

            conn.Open();

            string type = "";
            DateTime entryTime = DateTime.Now;
            double fee = 0;

            var selectQuery = "SELECT VehicleType, EntryTime FROM ParkedVehicles WHERE VehicleNumber=@num";
            using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
            {
                cmd.Parameters.AddWithValue("@num", number);

                // SqlDataReader - to read data row-by-row 
                using SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    Console.WriteLine("Vehicle not found!");
                    return;
                }
                type = reader.GetString(0);
                entryTime = reader.GetDateTime(1);
                reader.Close();

                Vehicle vehicle = type switch
                {
                    "Car" => new Car(number),
                    "Bike" => new Bike(number),
                    "Van" => new Van(number),
                    _ => null
                };
                // Reflection
                typeof(Vehicle)
                    .GetField("parkingInfo",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance)
                    .SetValue(vehicle, new ParkingInfo(entryTime));

                fee = vehicle.CalculateFees();
            }
            using SqlCommand cmd2 = new SqlCommand("RemoveVehicle", conn);
            cmd2.CommandType = System.Data.CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue("@VehicleNumber", number);
            cmd2.Parameters.AddWithValue("@ExitTime", DateTime.Now);
            cmd2.Parameters.AddWithValue("@Fee", fee);
            try
            {
                cmd2.ExecuteNonQuery();
                Console.WriteLine($"Vehicle exited. Fees to be paid: ${fee}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // View all parked vehicles
        public void ViewParkedVehicles()
        {
            using SqlConnection conn = DbConnection.Create();
            conn.Open();

            using SqlCommand cmd = new SqlCommand("ViewParkedVehicles", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader reader = cmd.ExecuteReader();

            List<(string Number, string Type)> vehicles = new List<(string, string)>();

            while (reader.Read())
            {
                vehicles.Add((reader.GetString(0), reader.GetString(1)));
            }

            if (!vehicles.Any())
            {
                Console.WriteLine("No vehicles parked.");
                return;
            }

            Console.WriteLine("Parked vehicles:");
            var sortedVehicles = vehicles.OrderBy(v => v.Number);

            foreach (var v in sortedVehicles)
            {
                Console.WriteLine($"{v.Number}({v.Type})");
            }
        }

        // Check if parking lot is empty using stored procedure
        public bool IsEmpty()
        {
            using SqlConnection conn = DbConnection.Create();
            conn.Open();
            using SqlCommand cmd = new SqlCommand("GetParkedCount", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // ExecuteScalar returns single value
            int count = (int)cmd.ExecuteScalar();
            return count == 0;
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

        public void UpdateVehicle(string oldNumber, string newNumber)
        {
            using SqlConnection conn = DbConnection.Create();
            conn.Open();
            using SqlCommand cmd = new SqlCommand("UpdateVehicleNumber", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OldNumber", oldNumber);
            cmd.Parameters.AddWithValue("@NewNumber", newNumber);

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Vehicle number updated successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
