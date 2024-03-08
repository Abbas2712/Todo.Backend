using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsImportant { get; set; } = false; 
        public bool IsTasked { get; set; } = true;
        public int? ListId { get; set; }
        public virtual TodoList? TodoList { get; set; }
    }
}
