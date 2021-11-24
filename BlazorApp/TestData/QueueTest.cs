using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class QueueTest
    {
        List<Error> err = new List<Error>()
        {
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
        };
        
        List<Reconciliation> recons = new List<Reconciliation>()
        {
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
        };
        
        List<Reconciliation> recons2 = new List<Reconciliation>()
        {
            new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
        };
        
        List<Error> err2 = new List<Error>()
        {
            new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
            new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
            new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
            new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
        };
        
        
        //Only takes tests from here and down!!
        
        
        List<Manager_test> managers = new List<Manager_test>()
        {
            new Manager_test(0, "DepartmentManager", 10000,"FINISHED",Convert.ToDateTime("01-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "INFO", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "INFO", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(1, "DepartmentManager", 10000,"FINISHED", Convert.ToDateTime("01-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "FAILED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(2, "RustyManager",10000,"FINISHED", Convert.ToDateTime("05-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(3, "CrustyManager",10000, "FINISHED", Convert.ToDateTime("07-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "WARNING", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "WARNING", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            }),
            new Manager_test(4, "EmployeeManager",10000, "FINISHED", Convert.ToDateTime("10-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(5, "FunnyManager",10000, "FINISHED", Convert.ToDateTime("21-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "ERROR", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            }),
            new Manager_test(6, "DepartmentManager",10000, "FINISHED", Convert.ToDateTime("01-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(7, "RustyManager",10000, "RUNNING", Convert.ToDateTime("05-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(8, "CrustyManager",10000, "QUEUED", Convert.ToDateTime("07-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "WARNING", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "WARNING", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            }),
            new Manager_test(9, "EmployeeManager",10000, "QUEUED", Convert.ToDateTime("10-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(10, "FunnyManager",10000, "QUEUED", Convert.ToDateTime("21-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "ERROR", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            }),
            new Manager_test(11, "DepartmentManager",10000, "QUEUED", Convert.ToDateTime("01-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(12, "RustyManager",10000, "QUEUED", Convert.ToDateTime("05-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(13, "CrustyManager",100000, "QUEUED", Convert.ToDateTime("07-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "WARNING", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "WARNING", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            }),
            new Manager_test(14, "EmployeeManager",100000, "QUEUED", Convert.ToDateTime("10-May-2021"), new List<Error>()
            {
                new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "FATAL", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")
            }, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
            }),
            new Manager_test(15, "FunnyManager",10000000, "QUEUED", Convert.ToDateTime("21-May-2021"), new List<Error>() {new Error(1, "INFO", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(2, "INFO", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Error(3, "WARNING", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Error(4, "ERROR", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Error(5, "ERROR", Convert.ToDateTime("21-May-2021"), "This is a message")}, new List<Reconciliation>()
            {
                new Reconciliation(1, "OK", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(2, "OK", Convert.ToDateTime("05-May-2021"), "This is a message"),
                new Reconciliation(3, "DISABLED", Convert.ToDateTime("01-May-2021"), "This is a message"),
                new Reconciliation(4, "OK", Convert.ToDateTime("10-May-2021"), "This is a message"),
                new Reconciliation(5, "MISMATCH", Convert.ToDateTime("21-May-2021"), "This is a message")
            })
        };
        
        public async Task<List<Manager_test>> ManagerList()
        {
            return await Task.FromResult(managers);
        }
    }
    
    
}