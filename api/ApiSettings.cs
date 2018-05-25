using System;

namespace api
{

    public class ApiSettings
    {
        public ApiSettings(){}

         public string ApplicationName { get; set; } = "News by the Numbers";
         public int RecentDaysLookback { get; set; } = 15;

    }

}