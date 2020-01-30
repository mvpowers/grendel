using System;
using System.IO;
using System.Reflection;
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
            
            var root = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            var settingsLocation = Path.Combine(root, "GrendelCommon", "appsettings.json");

            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile(settingsLocation);

            var config = configBuilder.Build();

            return config.GetConnectionString("GrendelDb");
        }
    }
}