using System;
using Microsoft.Data.SqlClient;

namespace Datastreaming
{
    //Forces the implementation of methods in the classes containing data from tables to ensure the methods can be called in TableStreamer
    //Every class that will store data from the stream has to implement this interface.
    public interface IData
    {
        void AddDataFromSqlReader(SqlDataReader reader);
        string GetChangesQueryString();
    }
}