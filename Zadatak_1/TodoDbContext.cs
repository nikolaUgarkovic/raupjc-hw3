using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<TodoItem> TodoItems { get; set; }
        public IDbSet<TodoItemLabel> TodoItemLabels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoItem>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.UserId).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(s => s.IsCompleted).IsRequired();

            modelBuilder.Entity<TodoItemLabel>().HasKey(t => t.Id);
            modelBuilder.Entity<TodoItemLabel>().Property(s => s.Value).IsRequired();

            modelBuilder.Entity<TodoItem>().HasMany(s => s.Labels).WithMany(m => m.LabelTodoItems);
        }
    }
}
