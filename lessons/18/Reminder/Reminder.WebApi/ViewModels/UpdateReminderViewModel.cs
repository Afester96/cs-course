using Reminder.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reminder.WebApi.ViewModels
{
    public class UpdateReminderViewModel
    {
        public ReminderItemStatus Status { get; set; }

        [Required]
        [StringLength(512)]
        public string Message { get; set; }
    }
}
