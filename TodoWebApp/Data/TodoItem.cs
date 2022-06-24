using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Data;

public class TodoItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Display(Name = "Created Time")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    [Required]
    [StringLength(25)]
    [Display(Name = "Task")]
    public string TaskDescription { get; set; }

    public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

    [Display(Name = "Completed")]
    public bool IsCompleted { get; set; } = false;
}

public enum PriorityLevel
{
    Low, Normal, High
}
