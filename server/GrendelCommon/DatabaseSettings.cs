using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GrendelCommon
{
    public static class DatabaseSettings
    {
        public static string GetConnectionString()
        {
            var environmentConnectionString = Environment.GetEnvironmentVariable("GRENDEL_CONNECTIONSTRING");

            if (!string.IsNullOrEmpty(environmentConnectionString))
                return environmentConnectionString;

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath($"{Environment.CurrentDirectory}/GrendelCommon")
                .AddJsonFile("appsettings.json");

            var config = configBuilder.Build();

            return config.GetConnectionString("GrendelDb");
        }
    }
}
