using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conta.Repository.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        


        public static string GetConnectionString()
        {
            var settings = GetConfig();

            return settings.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

       

    }
}
