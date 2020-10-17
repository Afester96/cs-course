using System;
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
		public void Get_GivenExistingItem_ShouldAddItemInDictionary()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var storage = new ReminderStorage();

			// Act
			storage.Add(item);
			var test = storage.Find(item.DateTime);

			// Assert
			Assert.Contains(item, test);
		}
		[Test]
		public void Get_GivenExistingItem_ShouldUpdateItemInDictionary()
		{
			// Arrange
			var itemId = Guid.NewGuid();
			var item = ReminderItem(itemId);
			var item2 = ReminderItem(itemId);
			var storage = new ReminderStorage(item);

			// Act
			storage.Update(item2);
			var test = storage.Find(item2.DateTime);

			// Assert
			Assert.Contains(item2, test);
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