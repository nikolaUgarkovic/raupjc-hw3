using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Zadatak_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((context, logger) =>
                {
                    var connectionString
                        = context.Configuration.GetConnectionString("DefaultConnection");
                    logger.MinimumLevel.Error().Enrich
                        .FromLogContext().WriteTo
                        .MSSqlServer(
                            connectionString,
                            "Errors", autoCreateSqlTable: true);
                })
                .Build();
    }
}
