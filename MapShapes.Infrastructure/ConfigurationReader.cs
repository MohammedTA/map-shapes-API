namespace MapShapes.Infrastructure
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationReader
    {
        public static IConfiguration GetConfigurations()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static string GetConnectionString()
        {
            return GetConfigurations().GetConnectionString("MapShapes");
        }
    }
}