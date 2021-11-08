namespace BlazorApp.Data
{
    public abstract class Log
    {
        protected int _id;

        public int Id
        {
            get
            {
                return _id;
            }
        }
        protected string _timestamp;


    }
}