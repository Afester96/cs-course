using System;
using System.Collections.Generic;

namespace Reminder.Receiver
{
	// "Hello" ->
	//  Text + DateTimeOffset

	public class MessagePayload
	{
		private static Dictionary<string, Func<double, TimeSpan>> WellKnownKeys =
			new Dictionary<string, Func<double, TimeSpan>>
			{
				["sec"] = TimeSpan.FromSeconds,
				["min"] = TimeSpan.FromMinutes,
				["hour"] = TimeSpan.FromHours,
				["day"] = TimeSpan.FromDays,
			};

		public DateTimeOffset DateTime { get; }
		public string Text { get; }

		public MessagePayload(string text, DateTimeOffset dateTime)
		{
			Text = text;
			DateTime = dateTime;
		}

		public static bool TryParse(string message, out MessagePayload payload)
		{
			payload = default;

			var parts = message.Split(new[] {"\n", "\t", ",", ";"}, StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 0)
			{
				return false;
			}

			var text = parts[0].Trim();
			if (parts.Length == 1)
			{
				payload = new MessagePayload(text, DateTimeOffset.UtcNow);
			}

			if (parts.Length > 1 && TryParseDateTime(parts[1], out var datetime))
			{
				payload = new MessagePayload(text, datetime);
			}

			return payload != default;
		}

		private static bool TryParseDateTime(string text, out DateTimeOffset datetime)
		{
			var parts = text.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			if (parts.Length == 2 && 
				int.TryParse(parts[0], out var count) &&
				WellKnownKeys.TryGetValue(parts[1].ToLower(), out var offset))
			{
				datetime = DateTimeOffset.UtcNow.Add(offset(count));
				return true;
			}

			return DateTimeOffset.TryParse(text, out datetime);
		}
	}
}