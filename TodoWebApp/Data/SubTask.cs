using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Data;
public class SubTask
{
    public int Id { get; set; }

    public string SubTaskDescription { get; set; }
}
