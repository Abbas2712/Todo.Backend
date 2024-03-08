﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models
{
    public class TodoList
    {
        public int Id { get; set; }
        public string ListName { get; set; }
        //public int Count { get; set; }
        public virtual ICollection<TodoItem> Items { get; set; }
    }
}
