using System;
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
            
            var basePath = Environment.CurrentDirectory.Replace("GrendelApi", "GrendelCommon");
            if (basePath == "/app") basePath = "/app/GrendelCommon";

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json");

            var config = configBuilder.Build();
            return config.GetConnectionString("GrendelDb");
        }
    }
}
