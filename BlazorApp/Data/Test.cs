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
            new HealthDataTest("CPU data", 70, "01:02"),
            new HealthDataTest("CPU data", 90, "01:03"),
            new HealthDataTest("CPU data", 87, "01:04"),
            new HealthDataTest("CPU data", 50, "01:05"),
            new HealthDataTest("CPU data", 87, "01:06"),
            new HealthDataTest("CPU data", 50, "01:07"),
            new HealthDataTest("CPU data", 70, "01:08"),
            new HealthDataTest("CPU data", 40, "01:09"),
            new HealthDataTest("CPU data", 70, "01:10"),
            new HealthDataTest("CPU data", 43, "01:11"),
            new HealthDataTest("CPU data", 20, "01:12"),
            new HealthDataTest("CPU data", 100, "01:13")
        };
        public async Task<List<HealthDataTest>> DataList()
        {
            return await Task.FromResult(usage);
        }

    }
}