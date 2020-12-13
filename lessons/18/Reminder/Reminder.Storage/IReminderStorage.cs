using System;
using System.Threading.Tasks;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage
{
	public interface IReminderStorage
	{
		/// <summary>
		/// Add new item in dictionary
		/// </summary>
		/// <param name="item"></param>
		Task AddAsync(ReminderItem item);

		/// <summary>
		/// Update item in dictionary
		/// </summary>
		/// <param name="item"></param>
		Task UpdateAsync(ReminderItem item);

		/// <summary>
		///   Returns item with matching by id
		/// </summary>
		/// <param name="id">The reminder id</param>
		/// <exception cref="ReminderItemNotFoundException">Raises if item with specified id is not found</exception>
		/// <returns>
		///   The reminder <see cref="ReminderItem"/>
		/// </returns>
		Task<ReminderItem> GetAsync(Guid id);

		/// <summary>
		/// Finding item by datetime
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		Task<ReminderItem[]> FindByAsync(ReminderItemFilter filter);
	}
}