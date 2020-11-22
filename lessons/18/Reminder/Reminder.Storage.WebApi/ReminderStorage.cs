using Reminder.Storage.Exceptions;
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

            return JsonSerializer.Deserialize<ReminderItem[]>(content);
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

            var dto = JsonSerializer.Deserialize<ReminderItem>(content);
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
            var asyncMessage = _client.GetAsync($"{ApiPrefix}/{item.Id:N}")
                .GetAwaiter()
                .GetResult();

            if (asyncMessage.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ReminderItemNotFoundException(item.Id);
            }

            var oldContent = asyncMessage.Content.ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            var dto = JsonSerializer.Deserialize<ReminderItem>(oldContent);
            
            var newReminderItem = new ReminderItem(
                dto.Id,
                item.Status,
                dto.DateTime,
                item.Message,
                dto.ContactId
                );

            var newJson = JsonSerializer.Serialize(newReminderItem);

            var newStringContent = new StringContent(newJson, Encoding.Unicode, "application/json");
            _client.DeleteAsync($"{ApiPrefix}/{item.Id:N}")
                .GetAwaiter()
                .GetResult();

            _client.PostAsync(ApiPrefix, newStringContent)
                .GetAwaiter()
                .GetResult();
        }
    }
}
