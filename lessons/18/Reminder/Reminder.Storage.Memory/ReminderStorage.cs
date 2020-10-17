using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.Storage.Memory
{
	using Exceptions;

	public class ReminderStorage : IReminderStorage
	{
		private readonly Dictionary<Guid, ReminderItem> _items;

		public ReminderStorage()
		{
			_items = new Dictionary<Guid, ReminderItem>();
		}

		public ReminderStorage(params ReminderItem[] items)
		{
			_items = items.ToDictionary(item => item.Id);
		}

		public void Add(ReminderItem item)
		{
			_items.Add(item.Id, item); 
		}

		public void Update(ReminderItem item)
		{
			_items[item.Id] = item;
		}

		public ReminderItem Get(Guid id)
		{
			// ContainsKey + indexer -> TryGetValue

			if (!_items.TryGetValue(id, out var item))
			{
				throw new ReminderItemNotFoundException(id);
			}

			return item;
		}

		public ReminderItem[] Find(DateTimeOffset datetime)
		{
			return _items.Values
				.Where(item => item.DateTime <= datetime)
				.OrderByDescending(item => item.DateTime)
				.ToArray();
		}
	}
}