using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [Required]
        [StringLength(25)]
        public string TaskDescription { get; set; }
        public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;
        public bool IsCompleted { get; set; } = false;
    }

    public enum PriorityLevel
    {
        Normal, Important
    }
}
