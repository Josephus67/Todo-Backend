namespace TodoApp.Models;
public class TodoRequestModels
{
    public class CreateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
    }
    
    public class UpdateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
    }
}