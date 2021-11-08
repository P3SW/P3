using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}