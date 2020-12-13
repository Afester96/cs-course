using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Reminder.Storage.Exceptions;
using Reminder.Tests;

namespace Reminder.Storage.Memory.Tests
{
	public class ReminderStorageTests
	{
		[Test]
		public void Get_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var itemId = Guid.NewGuid();

			var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.GetAsync(itemId)
			);
			Assert.AreEqual(itemId, exception.Id);
		}

		[Test]
		public async Task Get_GivenExistingItem_ShouldReturnIt()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Please();
			var storage = new ReminderStorage(item);

			// Act
			var result = await storage.GetAsync(itemId);

			// Assert
			Assert.AreEqual(itemId, result.Id);
		}

		[Test]
		public async Task Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			// Arrange
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage();

			// Act
			await storage.AddAsync(item);
			var result = await storage.GetAsync(item.Id);

			// Assert
			Assert.AreEqual(item.Id, result.Id);
		}

		[Test]
		public void Add_GivenExistingItem_ShouldRaiseException()
		{
			// Arrange
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage(item);

			// Act
			var exception = Assert.CatchAsync<ReminderItemAllreadyExistException>(() =>
				storage.AddAsync(item)
			);

			// Assert
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var item = Create.Reminder.Please();

			var exception = Assert.CatchAsync<ReminderItemNotFoundException>(() =>
				storage.UpdateAsync(item)
			);
			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public async Task Update_GivenExistingItem_ShouldReturnUpdatedValues()
		{
			// Arrange
			var item = Create.Reminder
				.WithMessage("Initial message")
				.WithContact("Initial contact")
				.Please();
			var storage = new ReminderStorage(item);

			// Act
			var updatedItem = Create.Reminder
				.WithId(item.Id)
				.WithMessage("Updated message")
				.WithContact("Updated contact")
				.Please();
			await storage.UpdateAsync(updatedItem);
			var result = await storage.GetAsync(item.Id);

			// Assert
			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ContactId, result.ContactId);
		}

		[Test]
		public async Task Find_GivenRemindersInFuture_ShouldReturnEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(1)),
				Create.Reminder.AtDatetime(datetime.AddSeconds(1))
			);

			var result = await storage.FindByAsync(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsEmpty(result);
		}

		[Test]
		public async Task Find_GivenRemindersInPastOrEqual_ShouldReturnNotEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(-1)),
				Create.Reminder.AtDatetime(datetime)
			);

			var result = await storage.FindByAsync(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsNotEmpty(result);
		}

		[Test]
		public async Task Find_GivenRemindersWithStatus_ShouldReturnNotEmptyCollection()
		{
			var status = ReminderItemStatus.Created;
			var storage = new ReminderStorage(
				Create.Reminder.WithStatus(status)
			);

			var result = await storage.FindByAsync(ReminderItemFilter.ByStatus(status));

			CollectionAssert.IsNotEmpty(result);
		}
	}
}