using System;
using System.Collections.Generic;

namespace Zadatak_1
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<TodoItemLabel> Labels { get; set; }
        public string Text { get; set; }

        public bool IsCompleted
        {
            get
            {
                return DateCompleted.HasValue;
            }
            set
            {

            }
        }

        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateDue { get; set; }

        public TodoItem()
        {
            // entity framework needs this one 
            // not for use
        }
        public TodoItem(string text)
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            Text = text;
        }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            Text = text;
            UserId = userId;
            Labels = new List<TodoItemLabel>();
        }

        public bool MarkAsCompleted()
        {
            if (IsCompleted) return false;
            DateCompleted = DateTime.UtcNow;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var item = (TodoItem)obj;
            return item.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TodoItemLabel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        public List<TodoItem> LabelTodoItems { get; set; }

        public TodoItemLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }
    }
}
