using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Reminder.WebApi
{
    using Microsoft.Extensions.Configuration;
    using Reminder.Storage;
    using Reminder.Storage.Exceptions;
    using Reminder.Storage.SqlServer;

    public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSingleton<IReminderStorage>(provider =>
				new ReminderStorage(_configuration.GetConnectionString("Database"))
			);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.Use(ReminderExceptionHandling);
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
		}

		private static async Task ReminderExceptionHandling(HttpContext context, Func<Task> next)
		{
			try
			{
				await next();
			}
			catch (ReminderItemNotFoundException)
			{
				context.Response.StatusCode = 404;
			}
			catch (ReminderItemAllreadyExistException)
			{
				context.Response.StatusCode = 409;
			}
		}
	}
}
