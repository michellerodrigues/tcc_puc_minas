
using DescarteService.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace DescarteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            //var saga = new DescarteSaga();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
