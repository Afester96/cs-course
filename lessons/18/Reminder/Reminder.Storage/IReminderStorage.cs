using System;
using System.Collections.Generic;
using Reminder.Storage.Exceptions;

namespace Reminder.Storage
{
	public interface IReminderStorage
	{
		/// <summary>
		/// Add new item in dictionary
		/// </summary>
		/// <param name="item"></param>
		void Add(ReminderItem item);

		/// <summary>
		/// Update item in dictionary
		/// </summary>
		/// <param name="item"></param>
		void Update(ReminderItem item);

		/// <summary>
		///   Returns item with matching by id
		/// </summary>
		/// <param name="id">The reminder id</param>
		/// <exception cref="ReminderItemNotFoundException">Raises if item with specified id is not found</exception>
		/// <returns>
		///   The reminder <see cref="ReminderItem"/>
		/// </returns>
		ReminderItem Get(Guid id);

		/// <summary>
		/// Finding item by datetime
		/// </summary>
		/// <param name="datetime"></param>
		/// <returns></returns>
		ReminderItem[] Find(DateTimeOffset datetime);
	}
}