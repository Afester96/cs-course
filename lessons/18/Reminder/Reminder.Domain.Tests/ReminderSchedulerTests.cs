using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Reminder.Sender;
using Reminder.Storage;
using Reminder.Tests;

namespace Reminder.Domain.Tests
{
	public class ReminderSchedulerTests
	{
		public ReminderSchedulerSettings DefaultSettings =>
			new ReminderSchedulerSettings
			{
				TimerDelay = TimeSpan.Zero,
				TimerInterval = TimeSpan.FromMilliseconds(20)
			};

		public IReminderSender SuccessSender =>
			new ReminderSender(fail: false);

		public IReminderSender FailedSender =>
			new ReminderSender(fail: true);

		public ReminderReceiver Receiver { get; } =
			new ReminderReceiver();

		public IReminderStorage Storage =>
			Create.Storage.Build();

		public ILogger<ReminderScheduler> Logger =>
			NullLogger<ReminderScheduler>.Instance;

		[Test]
		public async Task GivenReminderWithPastDateAndSuccessSender_ShouldRaiseSentEvent()
		{
			var raised = false;
			var scheduler = new ReminderScheduler(Logger, Storage, SuccessSender, Receiver);
			scheduler.ReminderSent += (sender, args) => raised = true;

			await scheduler.StartAsync(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}

		[Test]
		public async Task GivenReminderWithPastDateAndFailedSender_ShouldRaiseFailedEvent()
		{
			var raised = false;
			var scheduler = new ReminderScheduler(Logger, Storage, FailedSender, Receiver);
			scheduler.ReminderFailed += (sender, args) => raised = true;

			await scheduler.StartAsync(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}

		private void WaitTimers()
		{
			Thread.Sleep(DefaultSettings.TimerInterval.Milliseconds * 2);
		}
	}
}