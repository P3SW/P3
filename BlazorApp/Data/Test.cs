using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.DataStreaming;

namespace BlazorApp.Data
{
    public class Test
    {
        List<Reconciliation> recons = new List<Reconciliation>()
        {
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
            new HealthDataTest("CPU data", 70, Convert.ToDateTime("2021-09-09 12:12")),
            new HealthDataTest("CPU data", 90, Convert.ToDateTime("2021-09-09 12:13")),
            new HealthDataTest("CPU data", 87, Convert.ToDateTime("2021-09-09 12:14")),
            new HealthDataTest("CPU data", 50, Convert.ToDateTime("2021-09-09 12:15")),
            new HealthDataTest("CPU data", 87, Convert.ToDateTime("2021-09-09 12:16")),
            new HealthDataTest("CPU data", 50, Convert.ToDateTime("2021-09-09 12:17")),
            new HealthDataTest("CPU data", 70, Convert.ToDateTime("2021-09-09 12:18")),
            new HealthDataTest("CPU data", 40, Convert.ToDateTime("2021-09-09 12:19")),
            new HealthDataTest("CPU data", 70, Convert.ToDateTime("2021-09-09 12:20")),
            new HealthDataTest("CPU data", 43, Convert.ToDateTime("2021-09-09 12:21")),
            new HealthDataTest("CPU data", 20, Convert.ToDateTime("2021-09-09 12:22")),
            new HealthDataTest("CPU data", 100,Convert.ToDateTime("2021-09-09 12:23"))
        };
        public async Task<List<HealthDataTest>> DataList()
        {
            return await Task.FromResult(usage);
        }

    }
}