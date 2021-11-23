using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class Test // TEST DATA FOR RECONCILIATION TABLE
    {
        List<Reconciliation> recons = new List<Reconciliation>()
        {
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "OK: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "DISABLED: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "MISMATCH: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque dui ipsum, cursus eu pulvinar iaculis, semper sit amet leo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Integer mollis orci ac massa semper euismod vulputate non est. Quisque malesuada eleifend nunc, a efficitur urna suscipit vel. Suspendisse eu elementum nulla. Vestibulum nec quam sit amet ligula pulvinar viverra sed eget dui. Etiam purus tellus, gravida et est et, auctor convallis augue. Aliquam mi eros, molestie vitae odio quis, sollicitudin elementum justo. Nam euismod lectus nisi. Donec ut efficitur justo, sit amet pulvinar neque. Morbi convallis, justo nec tempor congue, leo velit sagittis ante, at vestibulum urna arcu vitae sapien. Ut a dui eu risus venenatis consequat. Proin pretium placerat est ac commodo. Suspendisse interdum, odio quis laoreet mollis, odio leo bibendum massa, et iaculis felis dolor eu ligula."),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"),
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
        };

        public async Task<List<Reconciliation>> ReconList()
        {
            return await Task.FromResult(recons);
        }

        private List<HealthDataTest> usage = new List<HealthDataTest>()
        {
            new HealthDataTest("CPU", 70, Convert.ToDateTime("2021-09-09 12:12")),
            new HealthDataTest("CPU", 90, Convert.ToDateTime("2021-09-09 12:13")),
            new HealthDataTest("CPU", 87, Convert.ToDateTime("2021-09-09 12:14")),
            new HealthDataTest("CPU", 50, Convert.ToDateTime("2021-09-09 12:15")),
            new HealthDataTest("CPU", 87, Convert.ToDateTime("2021-09-09 12:16")),
            new HealthDataTest("CPU", 50, Convert.ToDateTime("2021-09-09 12:17")),
            new HealthDataTest("CPU", 70, Convert.ToDateTime("2021-09-09 12:18")),
            new HealthDataTest("CPU", 40, Convert.ToDateTime("2021-09-09 12:19")),
            new HealthDataTest("CPU", 70, Convert.ToDateTime("2021-09-09 12:20")),
            new HealthDataTest("CPU", 43, Convert.ToDateTime("2021-09-09 12:21")),
            new HealthDataTest("CPU", 20, Convert.ToDateTime("2021-09-09 12:22")),
            new HealthDataTest("CPU", 100, Convert.ToDateTime("2021-09-09 12:23")),
            new HealthDataTest("MEMORY", 12, Convert.ToDateTime("2021-09-09 12:12")),
            new HealthDataTest("MEMORY", 41, Convert.ToDateTime("2021-09-09 12:13")),
            new HealthDataTest("MEMORY", 11, Convert.ToDateTime("2021-09-09 12:14")),
            new HealthDataTest("MEMORY", 59, Convert.ToDateTime("2021-09-09 12:15")),
            new HealthDataTest("MEMORY", 8, Convert.ToDateTime("2021-09-09 12:16")),
            new HealthDataTest("MEMORY", 5, Convert.ToDateTime("2021-09-09 12:17")),
            new HealthDataTest("MEMORY", 7, Convert.ToDateTime("2021-09-09 12:18")),
            new HealthDataTest("MEMORY", 10, Convert.ToDateTime("2021-09-09 12:19")),
            new HealthDataTest("MEMORY", 80, Convert.ToDateTime("2021-09-09 12:20")),
            new HealthDataTest("MEMORY", 3, Convert.ToDateTime("2021-09-09 12:21")),
            new HealthDataTest("MEMORY", 2, Convert.ToDateTime("2021-09-09 12:22")),
            new HealthDataTest("MEMORY", 10, Convert.ToDateTime("2021-09-09 12:23"))
        };

        public async Task<List<HealthDataTest>> DataList()
        {
            return await Task.FromResult(usage);
        }

        public void addClass()
        {
            recons.Add(new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message"));
        }
    }
}