namespace TodoApp.Models;
public class TodoRequestModels
{
    public class CreateTodoRequestModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
    }
    
    public class UpdateTodoRequestModel
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public bool IsCompleted { get; set; } = false;
    }
}