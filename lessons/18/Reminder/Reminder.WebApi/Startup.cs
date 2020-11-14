using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Reminder.WebApi
{
    using Reminder.Storage;
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.Memory;
    using System.Threading.Tasks;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IReminderStorage, ReminderStorage>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use((context, next) =>
            {
                try
                {
                    next();
                }
                catch (ReminderItemNotFoundException)
                {
                    context.Response.StatusCode = 404;
                }
                catch (ReminderItemAllreadyExistException)
                {
                    context.Response.StatusCode = 409;
                }

                return Task.CompletedTask;
            }
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
