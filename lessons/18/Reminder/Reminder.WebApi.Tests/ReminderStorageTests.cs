using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Reminder.Storage.Exceptions;
using Reminder.Tests;
using Reminder.WebApi.Tests;

namespace Reminder.Storage.WebApi.Tests
{
	public class ReminderStorageTests
	{
		[Test]
		public async Task WhenReminderCreated_ShouldReturnById()
		{
			using var factory = new ReminderWebApplicationFactory();
			var storage = factory.GetClient();
			var item = Create.Reminder
				.WithMessage("Something important")
				.WithContact("Contact")
				.Please();

			await storage.AddAsync(item);

			var result = await storage.GetAsync(item.Id);
			Assert.AreEqual(item.Message, result.Message);
			Assert.AreEqual(item.ContactId, result.ContactId);
		}

		[Test]
		public void WhenReminderCreated_IfItemExists_ShouldRaiseException()
		{
			using var factory = new ReminderWebApplicationFactory();
			var id = Guid.NewGuid();
			var storage = factory
				.WithItems(Create.Reminder.WithId(id))
				.GetClient();

			Assert.CatchAsync<ReminderItemAllreadyExistException>(() =>
				storage.AddAsync(Create.Reminder.WithId(id))
			);
		}

		[Test]
		public void WhenReminderFind_IfItemNotExists_ShouldRaiseException()
		{
			using var factory = new ReminderWebApplicationFactory();
			var id = Guid.NewGuid();
			var storage = factory.GetClient();

			Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.GetAsync(id)
			);
		}

		[Test]
		public async Task WhenReminderFind_IfItemExists_ShouldReturnIt()
		{
			using var factory = new ReminderWebApplicationFactory();
			var id = Guid.NewGuid();
			var storage = factory
				.WithItems(Create.Reminder.WithId(id))
				.GetClient();

			var item = await storage.GetAsync(id);

			Assert.AreEqual(id, item.Id);
		}

		[Test]
		public void WhenReminderUpdated_IfItemNotExists_ShouldRaiseException()
		{
			using var factory = new ReminderWebApplicationFactory();
			var id = Guid.NewGuid();
			var storage = factory.GetClient();

			Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.UpdateAsync(Create.Reminder.WithId(id))
			);
		}

		[Test]
		public async Task WhenReminderUpdated_IfItemExists_ShouldUpdateItem()
		{
			using var factory = new ReminderWebApplicationFactory();
			var id = Guid.NewGuid();
			var storage = factory
				.WithItems(Create.Reminder.WithId(id).WithMessage("Initial"))
				.GetClient();

			await storage.UpdateAsync(Create.Reminder.WithId(id).WithMessage("Updated"));

			var item = await storage.GetAsync(id);
			Assert.AreEqual("Updated", item.Message);
		}

		[Test]
		public async Task WhenReminderList_IfStatusSpecified_ShouldReturnMatchingItems()
		{
			using var factory = new ReminderWebApplicationFactory();
			var storage = factory
				.WithItems(
					Create.Reminder.WithStatus(ReminderItemStatus.Ready),
					Create.Reminder.WithStatus(ReminderItemStatus.Created))
				.GetClient();

			var items = await storage.FindByAsync(
				ReminderItemFilter.ByStatus(ReminderItemStatus.Ready)
			);

			Assert.AreEqual(1, items.Length);
		}

		[Test]
		public async Task WhenReminderList_IfDatetimeSpecified_ShouldReturnMatchingItems()
		{
			using var factory = new ReminderWebApplicationFactory();
			var storage = factory
				.WithItems(
					Create.Reminder.AtDatetime(DateTimeOffset.UtcNow.AddMinutes(-1)),
					Create.Reminder.AtDatetime(DateTimeOffset.UtcNow.AddMinutes(1))
				)
				.GetClient();

			var items = await storage.FindByAsync(ReminderItemFilter.CreatedAtNow());

			Assert.AreEqual(1, items.Length);
		}

		[Test]
		public async Task WhenReminderList_IfDatetimeAndStatusSpecified_ShouldReturnMatchingItems()
		{
			using var factory = new ReminderWebApplicationFactory();
			var storage = factory
				.WithItems(
					Create.Reminder
						.WithStatus(ReminderItemStatus.Created)
						.AtDatetime(DateTimeOffset.UtcNow.AddMinutes(-1)),
					Create.Reminder
						.WithStatus(ReminderItemStatus.Ready)
						.AtDatetime(DateTimeOffset.UtcNow.AddMinutes(1)),
					Create.Reminder
						.WithStatus(ReminderItemStatus.Ready)
						.AtDatetime(DateTimeOffset.UtcNow.AddMinutes(-1))
				)
				.GetClient();

			var items = await storage.FindByAsync(
				new ReminderItemFilter(DateTimeOffset.UtcNow, ReminderItemStatus.Ready)
			);

			Assert.AreEqual(1, items.Length);
		}
	}
}