using Microsoft.Data.SqlClient;

namespace DataStreamingSimulation
{
    public interface IHandleData
    {
        public void ApplyData(SqlDataReader reader);
    }
}