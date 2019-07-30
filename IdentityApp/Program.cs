using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HRApp.Core;
using HRApp.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HRApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
           IWebHost webHost = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                using (HRDbContext dbContext = scope.ServiceProvider.GetRequiredService<HRDbContext>())
                {
                    Seed.InvokeAsync(scope,dbContext).Wait();
                }
            }
                webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
