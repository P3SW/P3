using System;
using System.Collections.Generic;
using BlazorApp.Data;
using BlazorApp.Pages;
using Bunit;
using Xunit;
using Radzen.Blazor;

namespace P3ConversionDashboard.Tests
{
    public class UnitTestComponents : TestContext
    {
        //test data for LogDataTableTest
        private List<LogData> testLogData = new()
        {
            new LogData(new DateTime(1995, 1, 1,1,2,4), 
                "Manager has started", "UnitTestManager1","INFO"),
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
        RadzenDataGrid<LogData>? dataGridRef = null;

        //tests if the error div renders in Errors.razor
        [Fact]
        public void ErrorDivHasRenderedTest()
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

        //tests if the LogDataTable shows the LogData given.
        [Fact]
        public void LogDataTableTest()
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

            List<string> expectedOutputs = new()
            {
                $"<span class=\"rz-tabview-title\">{testGrades[0]}</span>", $"<span class=\"rz-tabview-title\">{testGrades[1]}</span>",
                $"<span class=\"rz-tabview-title\">{testGrades[2]}</span>", $"<span class=\"rz-tabview-title\">{testGrades[3]}</span>"
            };
            
            foreach (LogData log in testLogData)
            {
                expectedOutputs.Add(log.Grade);
                expectedOutputs.Add(log.ManagerName);
                expectedOutputs.Add(log.Timestamp.ToString());
                expectedOutputs.Add(log.Description);
            }

            foreach (string expectedOutput in expectedOutputs)
            {
                Assert.Contains(expectedOutput, renderedComponent);
            }
        }
        
        //tests if the LogDataList can sort between the different types of logs.
        [Fact]
        public void SortingTabsTest()
        {
            string[] testTabsSelectors =
            {
                "#log-tab-tabpanel-1-label", "#log-tab-tabpanel-2-label", 
                "#log-tab-tabpanel-3-label", "#log-tab-tabpanel-4-label"
            }; 
                
            JSInterop.Mode = JSRuntimeMode.Loose;
            var component = RenderComponent<LogDataTable>(parameterBuilder =>
            {

                parameterBuilder.Add(table => table.LogDataList, testLogData);
                parameterBuilder.Add(table => table.Grades, testGrades);
                parameterBuilder.Add(table => table.Type, "error");
                parameterBuilder.Add(table => table.GradeName, "Severity");
                parameterBuilder.Add(table => table.DataGridRef, dataGridRef);
            });

            foreach (string tab in testTabsSelectors)
            {
                var paraElm = component.Find(tab);
                paraElm.Click();
                var child = paraElm.Children;

                string renderedCut = component.Markup;

                foreach (LogData testData in testLogData)
                {
                    string[] testStrings =
                    {
                        testData.Description, testData.ManagerName,
                        testData.Timestamp.ToString()
                    };
                    
                    if (testData.Grade == child[0].InnerHtml)
                        foreach (string testString in testStrings)
                            Assert.Contains(testString, renderedCut);
                    else
                        foreach (string testString in testStrings)
                            Assert.DoesNotContain(testString, renderedCut);
                }
            }
        }
        
        //tests if the ConfigReader.cs reads the config file correct
        [Fact]
        public void ConfigReaderTest()
        {
            Assert.Equal("Server=localhost,1337;Database=ANS_DB_P3_TEST;Trust Server Certificate = true; User Id=sa;Password=Test123;Trusted_Connection=False",
                ConfigReader.ReadSetupFile("../../../ConfigReaderTestSetup.txt") );
        }
    }
}
