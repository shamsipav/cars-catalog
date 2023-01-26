using CarsCatalogAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalogAPI.Tests
{
    public class CustomWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CarsDBContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddSingleton<DbConnection>(container =>
                {
                    string workingDirectory = Environment.CurrentDirectory;
                    string projectAPIDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
                    var path = Path.Combine(projectAPIDirectory, @"CarsCatalogAPI\Cars.db");

                    var connection = new SqliteConnection($"Data Source={path}");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<CarsDBContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
