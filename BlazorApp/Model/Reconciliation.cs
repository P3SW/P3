namespace BlazorApp.Data
{
    public class Reconciliation : Log
    {

        public Reconciliation(int id, string result, string timestamp, string description)
        {
            _id = id;
            _result = result;
            _timestamp = timestamp;
            _description = description;
        }

        private string _result;
        private string _description;

        public string DecideResultColor()
        {
            switch (_result)
            {
                case "OK"       : return "color1";
                case "DISABLED" : return "color2";
                case "MISMATCH" : return "color3";
                case "FAILED"   : return "color4";
                default         : return "failColor";
            }
        }
        


    }
}