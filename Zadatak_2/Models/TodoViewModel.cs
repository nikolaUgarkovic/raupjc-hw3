using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Zadatak_1;

namespace Zadatak_2.Models
{
    public class TodoViewModel
    {
        public TodoViewModel(TodoItem todoItem)
        {
            Id = todoItem.Id;
            Text = todoItem.Text;
            DateDue = todoItem.DateDue;
            DateCompleted = todoItem.DateCompleted;

        }

        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime? DateCompleted { get; set; }

        public DateTime? DateDue { get; set; }

        public TodoViewModel()
        {
        }

        public string DaysTillDue()
        {
            string daysTillDue = "";
            if (DateDue != null)
            {
                daysTillDue = (((DateTime)DateDue - DateTime.Now).Days).ToString();
                daysTillDue = "(Još " + daysTillDue + " dana do roka!)";
            }
            return daysTillDue;
        }

        public string GetDate()
        {
            if (DateCompleted != null)
                return DateCompleted.ToString();
            else if (DateDue != null)
                return DateDue.ToString();
            else return "";
        }
    }
}
