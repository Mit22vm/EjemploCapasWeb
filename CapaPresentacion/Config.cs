using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion
{
    public static class Config
    {
        public static string getConnectionString
        {
            get {
                return Properties.Settings.Default.ConnectionString;
            }
        }
    }
}
