using Microsoft.Data.SqlClient;

namespace ParkingLot_Management.Data
{
    public static class DbConnection
    {
        private static readonly string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ParkingLotDB;Integrated Security=True";
        public static SqlConnection Create()
        {
            return new SqlConnection(connectionString);
        }
    }
}