using System;
using System.Collections.Generic;
using BlazorApp.Data;
using BlazorApp.Pages;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Xunit;
using Radzen.Blazor;
using Radzen;
using Radzen.Blazor.Rendering;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace P3ConversionDashboard.Tests
{
    public class Testtest : TestContext
    {
        private List<LogData> testLogData = new()
        {
            new LogData(new DateTime(1995, 1, 1,1,2,4), 
                "Manager has started", "UnitTestManager","INFO"),
            new LogData(new DateTime(2021, 10, 13,2,5,29),
                "Manager finished converting", "UnitTestManager2","INFO"),
            new LogData(new DateTime(2019, 12, 31,2,5,23),
                "Engine throttled, check cpu", "UnitTestManager3","WARN"),
            new LogData(new DateTime(2021, 4, 11,11,23,25),
                "Engine using a lot of memory", "UnitTestManager4","WARN"),
            new LogData(new DateTime(2018, 3, 1,5,23,13),
                "Manager failed data conversion", "UnitTestManager5","ERROR"),
            new LogData(new DateTime(2017, 6, 18,18,17,14),
                "Manager cannot convert data", "UnitTestManager6","ERROR"),
            new LogData(new DateTime(2011, 8, 14,4,7,19), 
                "Manager stopped suddenly", "UnitTestManager7","FATAL"),
            new LogData(new DateTime(2021, 9, 2,9,1,49),
                "Engine crashed!", "UnitTestManager8","FATAL")
        };
        
        List<string> testGrades = new() { "INFO", "WARN", "ERROR", "FATAL" };
        RadzenDataGrid<LogData> dataGridRef = null;
        
        private readonly ITestOutputHelper _testOutputHelper;

        public Testtest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        
        
        [Fact]
        public void ErrorDiv_Has_Rendered()
        {
            JSInterop.Mode = JSRuntimeMode.Loose;
            var sut = RenderComponent<Errors>();

            var renderedMarkup = sut.Markup;
            
            string expected = "<link href=\"https://fonts.googleapis.com/css?family=Roboto Slab\" rel=\"stylesheet\">\n\n" +
                              "<div class=\"log-table-formatting\">\r\n";
            string expected2 = "</div>";
            
            Assert.Contains(expected, renderedMarkup);
            Assert.Contains(expected2, renderedMarkup);
        }

        [Fact]
        public void LogDataTable_Rendered()
        {
            JSInterop.Mode = JSRuntimeMode.Loose; // because of radzen
            
            var component = RenderComponent<LogDataTable>(parameterBuilder =>
            {
                parameterBuilder.Add(table => table.LogDataList, testLogData);
                parameterBuilder.Add(table => table.Grades, testGrades);
                parameterBuilder.Add(table => table.Type, "error");
                parameterBuilder.Add(table => table.GradeName, "Severity");
                parameterBuilder.Add(table => table.DataGridRef, dataGridRef);
            });
            
            string renderedComponent = component.Markup;

            List<string> expectedOutputs = new List<string>()
            {
                $"<span class=\"rz-tabview-title\">{testGrades[0]}</span>", $"<span class=\"rz-tabview-title\">{testGrades[1]}</span>",
                $"<span class=\"rz-tabview-title\">{testGrades[2]}</span>", $"<span class=\"rz-tabview-title\">{testGrades[3]}</span>"
            };
            
            _testOutputHelper.WriteLine(new DateTime(1995, 1, 1,1,2,4).ToString());

            foreach (LogData log in testLogData)
            {
                expectedOutputs.Add(log.Grade);
                expectedOutputs.Add(log.ManagerName);
                expectedOutputs.Add(log.Timestamp.ToString("MM/dd/yyyy HH:mm:ss"));
                expectedOutputs.Add(log.Description);
            }

            foreach (string expectedOutput in expectedOutputs)
            {
                Assert.Contains(expectedOutput, renderedComponent);
            }
            
        }
    }
}
