using System.IO;
using Microsoft.Extensions.Configuration;

namespace ProjAndreAirlinesWebAPIDapper.Config
{
    public class DatabaseConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static string Get()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        
            Configuration = builder.Build();
            
            string connection = Configuration["ConnectionStrings:ProjAndreAirlinesWebAPIDapper"];
           
            return connection;
        }

    }
}
