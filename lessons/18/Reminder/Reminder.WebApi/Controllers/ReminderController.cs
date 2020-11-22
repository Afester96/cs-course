using Microsoft.AspNetCore.Mvc;
using Reminder.Storage;
using Reminder.Storage.Exceptions;
using Reminder.WebApi.ViewModels;
using System;
using System.Linq;

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
            var items = _storage.FindBy(
                new ReminderItemFilter(model?.DateTime, model?.Status)
            );
            return Ok(items);
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

            _storage.Add(item);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateReminderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
    }
}
