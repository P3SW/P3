using Microsoft.Data.SqlClient;

namespace SQLDatabaseRead
{
    public interface IDataHandler
    {
        void AddDataFromSqlReader(SqlDataReader reader);
        string GetNewestDataQueryString(string type);
    }
}