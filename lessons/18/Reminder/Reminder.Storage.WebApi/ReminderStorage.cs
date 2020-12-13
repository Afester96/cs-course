using Reminder.Storage.Exceptions;
using Reminder.Storage.WebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reminder.Storage.WebApi
{
	public class ReminderStorage : IReminderStorage
	{
		private const string ApiPrefix = "/api/reminders";
		private const string ContentType = "application/json";
		private static readonly JsonSerializerOptions SerializerOptions =
			new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};
		private static readonly JsonSerializerOptions DeserializerOptions =
			new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				Converters = { new JsonStringEnumConverter() }
			};

		private readonly HttpClient _client;

		public ReminderStorage(string endpoint)
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri(endpoint)
			};
		}

		public ReminderStorage(HttpClient client)
		{
			_client = client;
		}

		public async Task AddAsync(ReminderItem item)
		{
			var json = JsonSerializer.Serialize(new ReminderItemDto(item), SerializerOptions);
			var content = new StringContent(json, Encoding.Unicode, ContentType);
			var message = await _client.PostAsync(ApiPrefix, content);
			if (message.StatusCode == HttpStatusCode.Conflict)
			{
				throw new ReminderItemAllreadyExistException(item.Id);
			}

			message.EnsureSuccessStatusCode();
		}

		public async Task UpdateAsync(ReminderItem item)
		{
			var json = JsonSerializer.Serialize(new ReminderItemDto(item), SerializerOptions);
			var content = new StringContent(json, Encoding.Unicode, ContentType);
			var message = await _client.PutAsync($"{ApiPrefix}/{item.Id:N}", content);
			if (message.StatusCode == HttpStatusCode.NotFound)
			{
				throw new ReminderItemNotFoundException(item.Id);
			}

			message.EnsureSuccessStatusCode();
		}

		public async Task<ReminderItem> GetAsync(Guid id)
		{
			var message = await _client.GetAsync($"{ApiPrefix}/{id:N}");
			if (message.StatusCode == HttpStatusCode.NotFound)
			{
				throw new ReminderItemNotFoundException(id);
			}

			message.EnsureSuccessStatusCode();

			var json = await message.Content.ReadAsStringAsync();
			var dto = JsonSerializer.Deserialize<ReminderItemDto>(json, DeserializerOptions);

			return dto.ToItem();
		}

		public async Task<ReminderItem[]> FindByAsync(ReminderItemFilter filter)
		{
			var url = new StringBuilder(100)
				.Append(ApiPrefix)
				.Append('?');

			if (filter.Status.HasValue)
			{
				url.Append($"status={filter.Status.Value}&");
			}

			if (filter.DateTime.HasValue)
			{
				url.Append($"datetime={filter.DateTime.Value.ToUnixTimeMilliseconds()}&");
			}

			if (url[^1] == '&')
			{
				url.Remove(url.Length - 1, 1);
			}

			var message = await _client.GetAsync(url.ToString());
			var payload = await message.Content.ReadAsStringAsync();
			var dtos = JsonSerializer.Deserialize<List<ReminderItemDto>>(payload, DeserializerOptions);

			return dtos.Select(dto => dto.ToItem()).ToArray();
		}
	}
}
