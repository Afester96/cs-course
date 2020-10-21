using System;
using System.Collections.Generic;
using NUnit.Framework;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage.Memory.Tests
{
	public class ReminderStorageTests
	{
		[Test]
		public void Get_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var itemId = Guid.NewGuid();

			// var exception = Assert.Catch<ReminderItemNotFoundException>(Test);
			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Get(itemId)
			);
			Assert.AreEqual(itemId, exception.Id);
		}

		[Test]
		public void Get_GivenExistingItem_ShouldReturnIt()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var storage = new ReminderStorage(item);

			// Act
			var result = storage.Get(itemId);

			// Assert
			Assert.AreEqual(itemId, result.Id);
		}
		[Test]
		public void Add_GivenExistingItem_ShouldRaiseException()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var storage = new ReminderStorage(item);

			// Act
			var exception = Assert.Catch<ReminderItemAllreadyExistException>(() =>
			storage.Add(item));

			// Assert
			Assert.AreEqual(itemId, exception.Id);
		}
		[Test]
		public void Add_GivenNotExistingId_ShouldGetByIdAfterAdd()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var storage = new ReminderStorage();

			// Act
			storage.Add(item);
			var result = storage.Get(item.Id);

			// Assert
			Assert.AreEqual(item.Id, result.Id);
		}
		[Test]
		public void Update_GivenNotExistingId_ShouldRaiseException()
		{
			var storage = new ReminderStorage();
			var item = ReminderItem(Guid.NewGuid());

			var exception = Assert.Catch<ReminderItemNotFoundException>(() =>
				storage.Update(item)
			);
			Assert.AreEqual(item.Id, exception.Id);
		}
		[Test]
		public void Update_GivenExistingItem_ShouldReturnUpdatedValues()
		{
			// Arrange
			var item = ReminderItem(
				Guid.NewGuid(),
				message: "Initial message",
				contactId: "Initial contact");
			var storage = new ReminderStorage(item);

			// Act
			var updatedItem = ReminderItem(
				item.Id,
				item.Status,
				item.DateTime,
				message: "Updated message",
				contactId: "Updated contact");
			storage.Update(updatedItem);
			var result = storage.Get(item.Id);

			// Assert
			Assert.AreEqual(updatedItem.Message, result.Message);
			Assert.AreEqual(updatedItem.ContactId, result.ContactId);
		}

		public ReminderItem ReminderItem(
			Guid id,
			ReminderItemStatus status = ReminderItemStatus.Created,
			DateTimeOffset? datetime = default,
			string message = "message",
			string contactId = "contactId")
		{
			return new ReminderItem(id, status, datetime ?? DateTimeOffset.UtcNow, message, contactId);
		}

	}
}