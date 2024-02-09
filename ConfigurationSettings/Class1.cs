using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace ConfigurationSettings
{
    public static class ConfigurationSettings
    {
        private static readonly object ConfigurationManager;

        public static string MyFilePath => ConfigurationManager.AppSettings["MyFilePath"];
    }
}
