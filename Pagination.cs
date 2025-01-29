using Microsoft.Data.SqlClient;

namespace Sydvest_Bo
{
    public class Pagination
    {
        public (List<string> results, bool hasMore) GetPaginatedResults(string tableName, string columnName, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            List<string> results;
            bool hasMore;

            string query = $@"
                SELECT {columnName} 
                FROM {tableName}
                ORDER BY Id
                OFFSET @Offset ROWS 
                FETCH NEXT @PageSize ROWS ONLY;";

            var parameters = new Dictionary<string, object>
            {
                { "@Offset", (pageNumber - 1) * pageSize },
                { "@PageSize", pageSize }
            };

            results = SqlManager.ExecuteQuery(query, parameters, columnName);

            hasMore = results.Count == pageSize;

            return (results, hasMore);
        }
    }
}