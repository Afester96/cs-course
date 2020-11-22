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
            if (_items.ContainsKey(item.Id))
            {
                throw new ReminderItemAllreadyExistException(item.Id);
            }
            _items.Add(item.Id, item);
        }

        public void Update(ReminderItem item)
        {
            if (!_items.ContainsKey(item.Id))
            {
                throw new ReminderItemNotFoundException(item.Id);
            }
            _items[item.Id] = item;
        }

        public ReminderItem Get(Guid id)
        {
            if (!_items.TryGetValue(id, out var item))
            {
                throw new ReminderItemNotFoundException(id);
            }

            return item;
        }

        //public ReminderItem[] Find(DateTimeOffset datetime, ReminderItemStatus status)
        //{
        //    return _items.Values
        //        .Where(item => item.DateTime <= datetime && item.Status == status)
        //        .OrderByDescending(item => item.DateTime)
        //        .ToArray();
        //}

        public ReminderItem[] FindBy(ReminderItemFilter filter)
        {
            return _items.Values
                .Where(item => item.DateTime <= filter.DateTime && item.Status == filter.Status)
                .OrderByDescending(item => item.DateTime)
                .ToArray();
        }
    }
}