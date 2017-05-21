using System;
using System.Configuration;

namespace ApiTesting.CSharp.Framework
{
    public class GlobalConfiguration
    {
        public static Uri Url => new Uri(ConfigurationManager.AppSettings["Url"]);
    }
}
