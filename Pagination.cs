using Microsoft.Data.SqlClient;
namespace Sydvest_Bo;

public class Pagination
{
    private readonly string _connectionString = "Data Source=AsbLaptop\\SQLEXPRESS;Initial Catalog=sbdb;Integrated Security=True;Trust Server Certificate=True";

    public (List<string> results, bool hasMore) GetPaginatedResults(string tableName, string columnName, int pageNumber, int pageSize)
    {
        List<string> results = new();
        bool hasMore = false;
        string query = $@"
            SELECT {columnName} 
            FROM {tableName}
            ORDER BY Id
            OFFSET @Offset ROWS 
            FETCH NEXT @PageSize ROWS ONLY;";

        using (SqlConnection conn = new(_connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new(query, conn))
            {
                cmd.Parameters.AddWithValue("@Offset", (pageNumber - 1) * pageSize);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(reader[columnName].ToString());
                    }

                    hasMore = results.Count == pageSize;
                }
            }
        }

        return (results, hasMore);
    }
}