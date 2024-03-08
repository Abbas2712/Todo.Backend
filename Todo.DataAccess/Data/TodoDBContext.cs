using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DataAccess.Config;
using Todo.Models;

namespace Todo.DataAccess.Data
{
    public class TodoDBContext: DbContext
    {
        public TodoDBContext(DbContextOptions<TodoDBContext> options): base(options)
        {
            
        }

        public DbSet<TodoItem> todoItems { get; set; }
        public DbSet<TodoList> todoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemConfig());
            modelBuilder.ApplyConfiguration(new TodoListConfig());
        }
    }
}
