using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class AnotherTest // TEST DATA FOR ERROR TABLE
    {
        List<Error> err = new List<Error>()
        {
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a meesage"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5000, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
        };

        public async Task<List<Error>> ErrorList()
        {
            return await Task.FromResult(GetErrors());
        }
        public List<Error> GetErrors()
        {
            err.Add(new Error(5001, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"));
            err.Add(new Error(5002, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"));
            return err;
        }
    }
}