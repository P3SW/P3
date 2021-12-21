using System;
using System.Collections.Generic;
using BlazorApp.Data;

namespace P3ConversionDashboard.Tests.BlazorBackendTest
{
    public static class BlazorBackendCheckData
    {
        //test data for BlazorBackendIntegrationTest.cs
        public static List<TestManagerStatusHandler> testManagers = new List<TestManagerStatusHandler>()
        {
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.conversionUser.AnsConversionUserManager,rnd_2032045042",
                Error = new LogData(DateTime.Parse("2021-10-28 15:07:25.763"), "Executing pre-run scripts",
                    "DK.AES.ANS.KONVERTERING.MANAGERS.CONVERSIONUSER.ANSCONVERSIONUSERMANAGER","INFO"),
            },
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.init.ANSInitManager,rnd_-2028043180",
                CPU = new HealthData("CPU",6, DateTime.Parse("2021-10-28 15:08:28.723")),
                Memory = new HealthData("MEMORY", 6552645632, DateTime.Parse("2021-10-28 15:07:56.987")),
                Error = new LogData(DateTime.Parse("2021-10-28 15:07:50.517"), "Toggling foreign keys for Injury succeeded.",
                    "DK.AES.ANS.KONVERTERING.MANAGERS.INIT.ANSINITMANAGER","INFO"),
            },
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.actor.employees.EmployeesManager",
                CPU = new HealthData("CPU", 12, DateTime.Parse("2021-10-28 15:09:02.230")),
                Memory = new HealthData("MEMORY", 6566375424,DateTime.Parse("2021-10-28 15:09:02.230")),
                Error = new LogData(DateTime.Parse("2021-10-28 15:08:55.540"), "Executing pre-run scripts",  
                    "DK.AES.ANS.KONVERTERING.MANAGERS.ACTOR.EMPLOYEES.EMPLOYEESMANAGER","INFO"),
                Reconciliation = new LogData(DateTime.Parse("2021-10-28 15:09:05.513"),"ActorNoneHaveTypeIdTask",
                    "dk.aes.ans.konvertering.managers.actor.employees.EmployeesManager", "OK")
            },
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.actor.departments.DepartmentManager",
                Error = new LogData(DateTime.Parse("2021-10-28 15:09:11.627"), "Set nologging on tables: ACTOR, KONV_ACTOR",
                    "DK.AES.ANS.KONVERTERING.MANAGERS.ACTOR.DEPARTMENTS.DEPARTMENTMANAGER","INFO"),
                Reconciliation = new LogData(DateTime.Parse("2021-10-28 15:09:19.913"), "Mindst en række i ANS_KONV_BLOK22.dbo.[KONV_ACTOR]",
                    "dk.aes.ans.konvertering.managers.actor.departments.DepartmentManager", "OK")
            },
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.actor.membership.MembershipManager",
                CPU = new HealthData("CPU", 14, DateTime.Parse("2021-10-28 15:09:34.173")),
                Memory = new HealthData("MEMORY", 6563631104, DateTime.Parse("2021-10-28 15:09:34.187")),
                Error = new LogData(DateTime.Parse("2021-10-28 15:09:32.017"), "	Running  as user ANS_KONV_BLOK22.dbo",
                    "DK.AES.ANS.KONVERTERING.MANAGERS.ACTOR.MEMBERSHIP.MEMBERSHIPMANAGER", "INFO"),
                Reconciliation = new LogData(DateTime.Parse("2021-10-28 15:09:34.283"), "MembershipToDateEqual",
                    "dk.aes.ans.konvertering.managers.actor.membership.MembershipManager", "DISABLED")
            },
            new TestManagerStatusHandler()
            {
                managerName = "dk.aes.ans.konvertering.managers.parts.Konv.PartFSFTOpsaetningManager",
                Error = new LogData(DateTime.Parse("2021-10-28 15:09:40.270"), "Will clean AUDIT_LOGINFO where MGRNAME is 'dk.aes.ans.konvertering.managers.parts.Konv.PartFSFTOpsaetningManager'",
                    "DK.AES.ANS.KONVERTERING.MANAGERS.PARTS.KONV.PARTFSFTOPSAETNINGMANAGER", "INFO"),
            },
            
        };
    }
}