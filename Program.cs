using Microsoft.Data.SqlClient;

namespace Sydvest_Bo;

class Program
{
    public static string connectionString = @"Data Source=AsbLaptop\SQLEXPRESS;Initial Catalog=testbase;Integrated Security=True;Trust Server Certificate=True";
    static void Main(string[] args)
    {
        Connect();
        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }
    static void Connect()
    {
        try
        {
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            Console.WriteLine("Connection True");
            cnn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
