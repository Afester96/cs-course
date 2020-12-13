using System;
using System.Collections.Generic;
using System.Linq;

namespace Reminder.Storage.Memory
{
    using Exceptions;
    using System.Threading.Tasks;

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

        public Task AddAsync(ReminderItem item)
        {
            if (!_items.TryAdd(item.Id, item))
            {
                throw new ReminderItemAllreadyExistException(item.Id);
            }

            return Task.CompletedTask;
        }

        public Task UpdateAsync(ReminderItem item)
        {
            if (!_items.TryAdd(item.Id, item))
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            _items[item.Id] = item;

            return Task.CompletedTask;
        }

        public Task<ReminderItem> GetAsync(Guid id)
        {
            if (!_items.TryGetValue(id, out var item))
            {
                throw new ReminderItemNotFoundException(id);
            }

            return Task.FromResult(item);
        }

        public Task<ReminderItem[]> FindByAsync(ReminderItemFilter filter)
        {
            var line = _items.Select(pair => pair.Value);

            if (filter.Status.HasValue)
            {
                line = line.Where(item => item.Status == filter.Status.Value);
            }

            if (filter.DateTime.HasValue)
            {
                line = line.Where(item => item.DateTime <= filter.DateTime.Value);
            }

            return Task.FromResult(line.OrderByDescending(item => item.DateTime).ToArray());
        }
    }
}
