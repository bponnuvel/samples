using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SampleApp1.Infrastructure.Middlewares;

namespace SampleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseFailing(options =>
                {
                    options.ConfigPath = "/Failing";
                })
                .UseStartup<Startup>()
                .Build();
    }
}
