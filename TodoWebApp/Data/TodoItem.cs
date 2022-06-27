using System;
using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Data;

public class TodoItem
{
    public int Id { get; set; } 

    [Display(Name = "Created Time")]
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    [Required]
    [StringLength(25)]
    [Display(Name = "Task")]
    public string TaskDescription { get; set; }

    public PriorityLevel Priority { get; set; } = PriorityLevel.Normal;

    [Display(Name = "Completed")]
    public bool IsCompleted { get; set; } = false;

    public int SubTask { get; set; }                 // FK

    public List<SubTask> SubTasks { get; set; }  = new List<SubTask>();     // Hvis man glemmer at oprettet List-objektet, vil testen Correct_simulation_of_Disconnected_Data_in_Database() fejle!
}

public enum PriorityLevel
{
    Low, Normal, High
}
