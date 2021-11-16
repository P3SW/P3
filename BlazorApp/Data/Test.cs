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
            new HealthDataTest("CPU", 100,Convert.ToDateTime("2021-09-09 12:23")),
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
            new HealthDataTest("MEMORY", 10,Convert.ToDateTime("2021-09-09 12:23"))
        };
        public async Task<List<HealthDataTest>> DataList()
        {
            return await Task.FromResult(usage);
        }

    }
}