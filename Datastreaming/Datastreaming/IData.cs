using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    public interface IData
    {
        string GetChangesQueryString();
        void ConstructFromSqlReader(SqlDataReader reader);

    }
}