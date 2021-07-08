using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Dato")]
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        [Required]
        [StringLength(25)]
        [Display(Name = "Opgave")]
        public string TaskDescription { get; set; }

        [Display(Name = "Prioritet")]
        public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

        [Display(Name = "Udført")]
        public bool IsCompleted { get; set; } = false;
    }

    public enum PriorityLevel
    {
        Low, Normal, High
    }
}
