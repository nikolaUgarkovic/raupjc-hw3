using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1;
using Zadatak_1.Exceptions;


namespace Zadatak_1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }
        public TodoItem Get(Guid todoId, Guid userId)
        {
            var todoItem = _context.TodoItems.FirstOrDefault(x => x.Id.Equals(todoId));
            if (todoItem == null)
            {
                return null;
            }
            if (todoItem.UserId != userId)
            {
                throw new TodoAccessDeniedException();
            }
            return todoItem;
        }

        public void Add(TodoItem todoItem)
        {
            if (_context.TodoItems.Select(x => x.Id).Contains(todoItem.Id))
            {
                throw new DuplicateTodoItemException();
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            var todoItem = _context.TodoItems.FirstOrDefault(x => x.Id.Equals(todoId));
            if (todoItem != null)
            {
                if (todoItem.UserId != userId)
                {
                    throw new TodoAccessDeniedException();
                }
                _context.TodoItems.Remove(todoItem);
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (!_context.TodoItems.Contains(todoItem))
            {
                _context.TodoItems.Add(todoItem);
            }
            if (todoItem.UserId != userId)
            {
                throw new TodoAccessDeniedException();
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var todoItem = Get(todoId, userId);
            if (todoItem == null) return false;
            if (todoItem.UserId != userId)
            {
                throw new TodoAccessDeniedException();
            }
            todoItem.MarkAsCompleted();
            _context.Entry(todoItem).State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(x => x.UserId.Equals(userId)).OrderByDescending(x => x.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            return GetAll(userId).Where(x => !x.IsCompleted).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return GetAll(userId).Where(x => x.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return GetAll(userId).Where(filterFunction).ToList();
        }
    }
}
