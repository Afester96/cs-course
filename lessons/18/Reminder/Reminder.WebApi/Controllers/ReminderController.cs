using Microsoft.AspNetCore.Mvc;
using Reminder.Storage;
using Reminder.Storage.Exceptions;
using Reminder.WebApi.ViewModels;
using System;

namespace Reminder.WebApi.Controllers
{
    [Route("/api/reminders")]
    public class ReminderController : Controller
    {
        private readonly IReminderStorage _storage;

        public ReminderController(IReminderStorage storage)
        {
            _storage = storage;
        }

        [HttpGet("id")]
        public IActionResult Get(Guid id)
        {
				var item = _storage.Get(id);
				return Ok(item);
        }

		[HttpGet]
		public IActionResult Find([FromQuery] FindReminderViewModel model)
		{
			var items = _storage.Find(model.DateTime, model.Status);
			if (model.DateTime == null)
			{
				var item = model.Status;
				return Ok(item);
			}
			if (model.Status == ReminderItemStatus.Created && model.DateTime != null)
			{
				var item = model.Status;
				return Ok(item);
			}
			else
			{
				return Ok(items);
			}
		}

		[HttpPost]
		public IActionResult Create([FromBody] CreateReminderViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var item = new ReminderItem(
				model.Id ?? Guid.NewGuid(),
				model.Status,
				model.DateTime,
				model.Message,
				model.ContactId);

			try
			{
				_storage.Add(item);
				return CreatedAtAction("Get", new { id = item.Id }, item);
			}
			catch (ReminderItemAllreadyExistException)
			{
				return Conflict();
			}
		}

		[HttpPut("{id}")]
		public IActionResult Update(Guid id, [FromBody] UpdateReminderViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var item = _storage.Get(id);
				var updatedItem = new ReminderItem(
					item.Id,
					model.Status,
					item.DateTime,
					model.Message,
					item.ContactId);
				_storage.Update(updatedItem);
				return Ok(updatedItem);
			}

			catch (ReminderItemNotFoundException)
			{
				return NotFound();
			}
		}
	}
}
