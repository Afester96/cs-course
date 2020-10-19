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
		public void Get_GivenNewItem_ShouldRaiseException()
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
		public void Get_GivenNewItem_ShouldUpdateItemInDictionary()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var item2 = ReminderItem(itemId);
			var storage = new ReminderStorage(item);

			// Act
			storage.Update(item2);

			// Assert
			Assert;
		}

		public ReminderItem ReminderItem(Guid id)
		{
			return new ReminderItem(id, 
				ReminderItemStatus.Created,
				DateTimeOffset.UtcNow,
				"Message",
				"ContactId");
		}
		
	}
}