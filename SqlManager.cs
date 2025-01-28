using Microsoft.Data.SqlClient;

namespace Sydvest_Bo
{
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
                    Console.WriteLine("Connection Successful");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void Add(string tableName, string columns, string values)
        {
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            ExecuteQuery(query);
        }

        public static void Update(string tableName, string setClause, string condition)
        {
            string query = $"UPDATE {tableName} SET {setClause} WHERE {condition}";
            ExecuteQuery(query);
        }

        public static void Delete(string tableName, string condition)
        {
            string query = $"DELETE FROM {tableName} WHERE {condition}";
            ExecuteQuery(query);
        }

        private static void ExecuteQuery(string query)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}