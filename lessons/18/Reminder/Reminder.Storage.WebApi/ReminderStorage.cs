using Reminder.Storage.Exceptions;
using Reminder.Storage.WebApi.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Reminder.Storage.WebApi
{
    public class ReminderStorage : IReminderStorage
    {
        private const string ApiPrefix = "/api/reminders";
        private readonly HttpClient _client;
        public ReminderStorage(string endpoint)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(endpoint)
            };
        }
        public void Add(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var message = _client.PostAsync(ApiPrefix, content)
                .GetAwaiter()
                .GetResult();

            if (message.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ReminderItemAllreadyExistException(item.Id);
            }
        }

        public ReminderItem[] FindBy(ReminderItemFilter filter)
        {
            var message = _client.GetAsync($"{ApiPrefix}/{filter.DateTime:N}")
                .GetAwaiter()
                .GetResult();

            var content = message.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var dto = JsonSerializer.Deserialize<ReminderItemDto[]>(content);
            var array = new ReminderItem[dto.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new ReminderItem(dto[i].Id,dto[i].Status,dto[i].DateTime,dto[i].Message,dto[i].ContactId);
            }
            return array;
        }

        public ReminderItem Get(Guid id)
        {
            var message = _client.GetAsync($"{ApiPrefix}/{id:N}")
                .GetAwaiter()
                .GetResult();

            if (message.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ReminderItemNotFoundException(id);
            }

            var content = message.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var dto = JsonSerializer.Deserialize<ReminderItemDto>(content);
            return new ReminderItem(
                dto.Id,
                dto.Status,
                dto.DateTime,
                dto.Message,
                dto.ContactId
                );
        }

        public void Update(ReminderItem item)
        {
            var json = JsonSerializer.Serialize(item);
            var content = new StringContent(json, Encoding.Unicode, "application/json");
            var message = _client.PutAsync($"{ApiPrefix}/{item.Id:N}",content)
                .GetAwaiter()
                .GetResult();
            
            if (message.StatusCode == HttpStatusCode.Conflict)
            {
                throw new ReminderItemAllreadyExistException(item.Id);
            }
        }
    }
}
