using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Reminder.Storage;
using Reminder.Storage.WebApi;

namespace Reminder.WebApi.Tests
{
	public class ReminderWebApplicationFactory : WebApplicationFactory<Startup>
	{
		private ReminderItem[] _existingItems = Array.Empty<ReminderItem>();

		public ReminderStorage GetClient() =>
			new ReminderStorage(CreateClient());

		public ReminderWebApplicationFactory WithItems(params ReminderItem[] items)
		{
			_existingItems = items;
			return this;
		}

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureTestServices(services =>
			{
				var descriptor = ServiceDescriptor.Singleton<IReminderStorage>(
					provider => new Storage.Memory.ReminderStorage(_existingItems)
				);
				services.Replace(descriptor);
			});
		}
	}
}
