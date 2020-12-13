using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Reminder.Storage.SqlServer
{
	using Exceptions;

	public class ReminderStorage : IReminderStorage
	{
		private readonly string _connection;

		public ReminderStorage(string connection)
		{
			_connection = connection;
		}

		public async Task AddAsync(ReminderItem item)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "AddReminderItem";
			command.Parameters.AddWithValue("id", item.Id);
			command.Parameters.AddWithValue("dateTime", item.DateTime);
			command.Parameters.AddWithValue("statusId", (int)item.Status);
			command.Parameters.AddWithValue("message", item.Message);
			command.Parameters.AddWithValue("chatId", item.ContactId);

			try
			{
				await command.ExecuteNonQueryAsync();
			}
			catch (SqlException exception) when (exception.Number == 2627)
			{
				throw new ReminderItemAllreadyExistException(item.Id, exception);
			}
		}

		public async Task UpdateAsync(ReminderItem item)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateCommand();
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "UpdateReminderItem";
			command.Parameters.AddWithValue("id", item.Id);
			command.Parameters.AddWithValue("statusId", (int)item.Status);
			command.Parameters.AddWithValue("message", item.Message);
			var rows = command.Parameters.Add("rows", SqlDbType.Int);
			rows.Direction = ParameterDirection.Output;

			await command.ExecuteNonQueryAsync();

			if ((int)rows.Value == 0)
			{
				throw new ReminderItemNotFoundException(item.Id);
			}
		}

		public async Task<ReminderItem> GetAsync(Guid key)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = @"
SELECT 
  RI.Id,
  RI.DateTime,
  RI.StatusId AS Status,
  RI.Message,
  RC.ChatId AS ContactId
  FROM [ReminderItem] RI
  JOIN [ReminderContact] RC
    ON RI.ContactId = RC.Id
 WHERE RI.Id = @id
";
			command.Parameters.AddWithValue("id", key);

			var reminder = await ReadRemindersAsync(command).FirstOrDefaultAsync();
			if (reminder is null)
			{
				throw new ReminderItemNotFoundException(key);
			}

			return reminder;
		}

		public async Task<ReminderItem[]> FindByAsync(ReminderItemFilter filter)
		{
			await using var connection = await GetConnection();
			await using var command = connection.CreateCommand();

			var conditions = new List<string>();

			if (filter.Status.HasValue)
			{
				conditions.Add(" RI.StatusId = @status ");
				command.Parameters.AddWithValue("status", (int)filter.Status);
			}

			if (filter.DateTime.HasValue)
			{
				conditions.Add(" RI.DateTime >= @datetime ");
				command.Parameters.AddWithValue("datetime", filter.DateTime);
			}

			var query = @"
SELECT 
  RI.Id,
  RI.DateTime,
  RI.StatusId AS Status,
  RI.Message,
  RC.ChatId AS ContactId
  FROM [ReminderItem] RI
  JOIN [ReminderContact] RC
    ON RI.ContactId = RC.Id
";

			if (conditions.Count > 0)
			{
				query += "WHERE ";
				query += string.Join("AND", conditions);
			}

			command.CommandType = CommandType.Text;
			command.CommandText = query;

			return await ReadRemindersAsync(command).ToArrayAsync();
		}

		private async IAsyncEnumerable<ReminderItem> ReadRemindersAsync(SqlCommand command)
		{
			await using var reader = await command.ExecuteReaderAsync();

			if (!reader.HasRows)
			{
				yield break;
			}

			var id = reader.GetOrdinal("Id");
			var dateTime = reader.GetOrdinal("DateTime");
			var status = reader.GetOrdinal("Status");
			var message = reader.GetOrdinal("Message");
			var contactId = reader.GetOrdinal("ContactId");

			while (await reader.ReadAsync())
			{
				var reminder = new ReminderItem(
					reader.GetGuid(id),
					(ReminderItemStatus)reader.GetInt32(status),
					reader.GetDateTimeOffset(dateTime),
					reader.GetString(message),
					reader.GetString(contactId)
				);

				yield return reminder;
			}
		}

		private async Task<SqlConnection> GetConnection()
		{
			var connection = new SqlConnection(_connection);
			await connection.OpenAsync();
			return connection;
		}
	}
}