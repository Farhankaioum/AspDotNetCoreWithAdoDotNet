using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FormUI
{
    public static class Helper
    {
        public static string CnnVal(string connectionName)
        {
            
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString; 
        }
    }
}
