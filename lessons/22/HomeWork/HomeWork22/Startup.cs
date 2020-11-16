using HomeWork22.Model;
using HomeWork22.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HomeWork22
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddSingleton<IStorage, Storage>();

			services
				.AddControllers()
				.AddXmlSerializerFormatters();
			
			services.AddSwaggerGen(options =>
				options.SwaggerDoc("cities",
					new OpenApiInfo
					{
						Version = "v2",
						Title = "Cities API",
					}
				)
			);
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("cities/swagger.json", "Cities API"));
			app.UseRouting();
			app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
		}
	}
}