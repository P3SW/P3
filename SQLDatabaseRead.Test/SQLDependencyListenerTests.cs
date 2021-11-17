using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Autofac.Extras.Moq;
using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using Moq;
using Xunit;

namespace SQLDatabaseRead.Test
{
    public class SQLDependencyListenerTests
    {
        // [Fact]
        // public void DatabaseListenerQueryStrings_ValidCall()
        // {
        //     string ErrorSelectQuery = DatabaseListenerQueryStrings.ErrorSelect;
        //     string HealthSelectQuery = DatabaseListenerQueryStrings.HealthSelect;
        //     string ManagersSelectQuery = DatabaseListenerQueryStrings.ManagersSelect;
        //     string ReconciliationSelectQuery = DatabaseListenerQueryStrings.ReconciliationSelect;
        //     string ManagerSelectQuery = DatabaseListenerQueryStrings.ManagerTrackingSelect;
        //
        //     
        //     
        //     using (var reader = MockIDataReader())
        //     {
        //         // AutoMock framework for creating fake objects
        //         using (var mock = AutoMock.GetLoose())
        //         {
        //             mock.Mock<IDataHandler>()
        //                 .Setup(x => x.AddDataFromSqlReader(reader));
        //
        //             var cls = mock.Create<HealthDataHandler>();
        //
        //             cls.AddDataFromSqlReader(reader);
        //             
        //             mock.Mock<IDataHandler>()
        //                 .Verify(x=>x.AddDataFromSqlReader(reader), Times.AtLeast(1));
        //         }
        //     }
        // }
        //
        // private IDataReader MockIDataReader()
        // {
        //     var moq = new Mock<IDataReader>();
        //
        //     bool readToggle = true;
        //
        //     moq.Setup(x => x.Read())
        //         .Returns(() => readToggle)
        //         .Callback(() => readToggle = false);
        //
        //     moq.Setup(x => x["Char"])
        //         .Returns(null);
        //
        //     return moq.Object;
        // }
        
        // [Fact]
        // public void GetSelectStringsForTableStreamer_ValidCall()
        // {
        //     DateTime startTime = DateTime.Now;
        //     DateTime endTime = DateTime.Now;
        //     ManagerStatusHandler managerStatusHandler = new ManagerStatusHandler("TestManager", 123);
        //     managerStatusHandler.GetSelectStringsForTableStreamer("health");
        //     managerStatusHandler.GetSelectStringsForTableStreamer("logging");
        //     managerStatusHandler.GetSelectStringsForTableStreamer("reconciliation");
        // }
    }
}