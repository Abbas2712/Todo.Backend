using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Models.DTOS
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
        [DefaultValue(false)]
        public bool IsImportant { get; set; }
        [DefaultValue(true)]
        public bool IsTasked { get; set; }

    }
}
