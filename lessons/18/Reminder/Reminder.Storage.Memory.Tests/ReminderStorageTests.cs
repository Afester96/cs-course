using System;
using System.Collections.Generic;
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

			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Get(itemId)
			);

			Assert.AreEqual(itemId, exception.Id);
		}

		[Test]
		public void Get_GivenExistingItem_ShouldReturnIt()
		{
			var itemId = Guid.NewGuid();
			var item = Create.Reminder.WithId(itemId).Please();
			var storage = new ReminderStorage(item);

			var result = storage.Get(itemId);

			Assert.AreEqual(itemId, result.Id);
		}

		[Test]
		public void Add_GivenExistingItem_ShouldRaiseException()
		{
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage(item);

			var exception = Assert.Catch<ReminderItemAllreadyExistException>(() =>
				storage.Add(item)
			);

			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			var item = Create.Reminder.Please();
			var storage = new ReminderStorage();

			storage.Add(item);
			var result = storage.Get(item.Id);

			Assert.AreEqual(item.Id, result.Id);
		}

		[Test]
		public void Update_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var item = Create.Reminder.Please();

			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Update(item));

			Assert.AreEqual(item.Id, exception.Id);
		}

		[Test]
		public void Update_GivenExistingItem_ShouldReturnUpdatedValues()
		{
			var item = Create.Reminder
				.WithMessage("Initial message")
				.WithContact("Initial contact")
				.Please();
			var storage = new ReminderStorage(item);

			var updatedItem = Create.Reminder
				.WithId(item.Id)
				.WithMessage("Updated message")
				.WithContact("Updated contact")
				.Please();
			storage.Update(updatedItem);
			var result = storage.Get(item.Id);

			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ContactId, result.ContactId);
		}

		[Test]
		public void Find_GivenRemindersInFuture_ShouldReturnEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(1)),
				Create.Reminder.AtDatetime(datetime.AddSeconds(1))
			);

			var result = storage.FindBy(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsEmpty(result);
		}

		[Test]
		public void Find_GivenRemindersInPastOrEqual_ShouldReturnNotEmptyCollection()
		{
			var datetime = DateTimeOffset.UtcNow;
			var storage = new ReminderStorage(
				Create.Reminder.AtDatetime(datetime.AddMinutes(-1)),
				Create.Reminder.AtDatetime(datetime)
			);

			var result = storage.FindBy(ReminderItemFilter.CreatedAt(datetime));

			CollectionAssert.IsNotEmpty(result);
		}
	}
}