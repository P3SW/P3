using BlazorApp.Data;

namespace P3ConversionDashboard.Tests.BlazorBackendTest
{
    //a data class that is used for the test
    public class TestManagerStatusHandler
    {
        public string managerName;
        public HealthData CPU;
        public HealthData Memory;
        public LogData Error;
        public LogData Reconciliation;
    }
}