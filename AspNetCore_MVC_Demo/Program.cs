using AspNetCore_MVC_Demo.Data;
using AspNetCore_MVC_Demo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore_MVC_Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    var defaultUser = new User() { Username = "default", Email = "default@email.com" };
                    context.Users.Add(defaultUser);

                    context.AddRange(new List<Todo>{
                        new Todo() { User=defaultUser, Mission="Walk the dog", CreatedOn=DateTime.Now },
                        new Todo() { User=defaultUser, Mission="By a house plant", CreatedOn=DateTime.Now.AddDays(-2), EndedOn=DateTime.Now, IsDone=true },
                        new Todo() { User=defaultUser, Mission="Fix the roof", CreatedOn=DateTime.Now.AddDays(-1) },
                    });

                    context.SaveChanges();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
