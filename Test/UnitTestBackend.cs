using BlazorApp.Data;
using Xunit;
using Xunit.Abstractions;

namespace P3ConversionDashboard.Tests
{
    public class UnitTestBackend
    {
        
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTestBackend(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ConfigReaderTest()
        {
            Assert.Equal("Server=localhost,1337;Database=ANS_DB_P3_TEST;Trust Server Certificate = true; User Id=sa;Password=Test123;Trusted_Connection=False",
                ConfigReader.ReadSetupFile("../../../testSetup.txt") );
        }
    }
}