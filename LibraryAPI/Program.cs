using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(Configure)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        
        private static void Configure(IConfigurationBuilder builder) =>                     // décclaration de l'emplacement du fichier de config appsetting.json car                    
            builder.SetBasePath(Directory.GetCurrentDirectory())                            // on va le ranger ailleur, mais il faut le déclarer quand même de toutes façon
                .AddJsonFile(Path.Combine("Config", "appsettings.json"))
                .Build();
    }
}
