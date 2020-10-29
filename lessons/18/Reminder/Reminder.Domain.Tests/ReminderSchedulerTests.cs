using System;
using System.Threading;
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

		[Test]
		public void GivenReminderWithPastDateAndSuccessSender_ShouldRaiseSentEvent()
		{
			var raised = false;
			using var scheduler = new ReminderScheduler(Storage, SuccessSender, Receiver);
			scheduler.ReminderSent += (sender, args) => raised = true;

			scheduler.Start(DefaultSettings);
			Receiver.SendMessage(DateTimeOffset.UtcNow, "Message", "ContactId");
			WaitTimers();

			Assert.IsTrue(raised);
		}

		[Test]
		public void GivenReminderWithPastDateAndFailedSender_ShouldRaiseFailedEvent()
		{
			var raised = false;
			using var scheduler = new ReminderScheduler(Storage, FailedSender, Receiver);
			scheduler.ReminderFailed += (sender, args) => raised = true;

			scheduler.Start(DefaultSettings);
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