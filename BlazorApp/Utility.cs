using System.Linq;
using BlazorApp.Data;

namespace BlazorApp
{
    public static class Utility
    {
        /* Shortens the name of a manager */
        public static string ShortenManagerName(string name)
        {
            return name.Split('.').Last();
        }
        
        /* Calculates the runtime from ms to hh:mm:ss format */
        public static string FormatTime(int time)
        {
            string seconds = (time / 1000 % 60).ToString();
        
            string minutes = (((time / 1000) / 60) % 60).ToString();
        
            string hours = (time / 3600 / 1000).ToString();

            if (seconds.Length < 2)
            {
                seconds = "0" + seconds;
            }
            if (minutes.Length < 2)
            {
                minutes = "0" + minutes;
            }
            if (hours.Length < 2)
            {
                hours = "0" + hours;
            }

            if (hours == "00")
            {
                return minutes + ':' + seconds;
            } else
                return hours + ':' + minutes + ':' + seconds;
        }
    }
}