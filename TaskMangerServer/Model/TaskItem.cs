using System.ComponentModel.DataAnnotations;

namespace TaskMangerServer.Model
{
    public class TaskItem
    {
        [Key]
        public int TaskItemId { get; set; }
        public string? TaskName { get; set; }
        public bool IsComplete { get; set; }
    }
}
