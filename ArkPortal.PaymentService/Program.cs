using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ArkPotal.PaymentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseUrls("http://localhost:50622")
                    .UseStartup<Startup>();
                });
    }
}
