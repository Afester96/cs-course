using Reminder.Storage;
using Reminder.Storage.Memory;

namespace Reminder.Domain.Tests
{
	public class ReminderStorageBuilder
	{
		private ReminderItem[] _items = new ReminderItem[0];

		public ReminderStorageBuilder WithItems(params ReminderItem[] items)
		{
			return this;
		}

		public ReminderStorage Build()
		{
			return new ReminderStorage(_items);
		}
	}
}