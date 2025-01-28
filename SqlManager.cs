using Microsoft.Data.SqlClient;

namespace Sydvest_Bo;

public class SqlManager
{
    public static string connectionString = @"Data Source=AsbLaptop\SQLEXPRESS;Initial Catalog=sbdb;Integrated Security=True;Trust Server Certificate=True";
    public static void Connect()
    {
        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                Console.WriteLine("Connection True");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
