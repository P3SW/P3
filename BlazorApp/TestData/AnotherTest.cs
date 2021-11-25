using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class AnotherTest // TEST DATA FOR ERROR TABLE
    {
        List<Error> err = new List<Error>()
        {
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "INFO: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "WARNING: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "FATAL: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "ERROR: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message"),
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is an error message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is not an error message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is an error message"),
            new Error(5000, "ERROR", Convert.ToDateTime("21-May-2021"), "This is an error message")
        };

        public async Task<List<Error>> ErrorList()
        {
            return await Task.FromResult(GetErrors());
        }
        public List<Error> GetErrors()
        {
            err.Add(new Error(5001, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"));
            err.Add(new Error(5002, "INFO", Convert.ToDateTime("01-May-2021"), "This is an error message"));
            return err;
        }
    }
}