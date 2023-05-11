using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CustomerDetails_Next_Prev_First_Last
{
    public class ReadCS
    {
        public static string ConStr
        {
            get { return ConfigurationManager.ConnectionStrings["cs"].ConnectionString; }        
        }
    }
}