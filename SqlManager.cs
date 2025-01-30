using System.Text;
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
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ('{values}')";
            ExecuteNonQuery(query);
        }

        public static void Update(string tableName, string setClause, string condition)
        {
            string query = $"UPDATE {tableName} SET {setClause} WHERE {condition}";
            ExecuteNonQuery(query);
        }

        public static void Delete(string tableName, string condition)
        {
            string query = $"DELETE FROM {tableName} WHERE {condition}";
            ExecuteNonQuery(query);
        }

        private static void ExecuteNonQuery(string query)
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

        public static List<string> ExecuteQuery(string query, Dictionary<string, object> parameters, string columnName)
        {
            List<string> results = new();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StringBuilder combinedResult = new();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                combinedResult.Append(reader[i] + "   ");
                            }

                            results.Add(combinedResult.ToString());
                        }
                    }
                }
            }

            return results;
        }

        public static List<string> GetAllRegions()
        {
            string query = "SELECT region_name FROM Region";
            var parameters = new Dictionary<string, object>();
            return ExecuteQuery(query, parameters, "region_name");
        }

        public static void Close()
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Close();
            }
        }
    }
}
