using System.Net.Http;
using Reminder.Sender.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Reminder.Sender.Telegram
{
	public class ReminderSender : IReminderSender
	{
		private readonly ITelegramBotClient _client;

		public ReminderSender(string token)
		{
			_client = new TelegramBotClient(token);
		}

		public void Send(ReminderNotification item)
		{
			string text;
			if (item.Message == "info")
			{
				text = @"Hello! If you want to use reminder, please write this:
<Message>
<DateTimeUtc>
Messages without datetime returns imideatly";
			}
			else
			{
				text = $"{item.Message} at {item.DateTime:R}";
			}
			var chatId = new ChatId(long.Parse(item.ContactId));

			try
			{
				_client.SendTextMessageAsync(chatId, text)
					.GetAwaiter()
					.GetResult();
			}
			catch (HttpRequestException exception)
			{
				throw new ReminderSenderException(exception);
			}
		}
	}
}