using MongoDB.Bson;
using MongoDB.Driver;
using TodoApp.Models;

namespace TodoApp.Providers;
public class Dataprovider(IMongoCollection<Todo> _todoCollection) : IDataprovider
{
    public async Task<TodoResponseModels> CreateTodoAsync(TodoRequestModels.CreateTodoRequest todo)
    {
        var newTodo = new Todo
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Title = todo.Title,
            Description = todo.Description,
            DueDate = todo.DueDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsCompleted = false,
            IsDeleted = false
        };
        await _todoCollection.InsertOneAsync(newTodo);
        return MapToResponse(newTodo);
    }

    public async Task<List<TodoResponseModels>> GetAllTodosAsync()
    {
        var todos = await _todoCollection.Find(t => !t.IsDeleted).ToListAsync();
        return todos.Select(MapToResponse).ToList();
    }

    public async Task<TodoResponseModels?> GetTodoByIdAsync(string id)
    {
        var todo = await _todoCollection.Find(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
        return todo == null ? null : MapToResponse(todo);
    }

    public async Task<TodoResponseModels?> UpdateTodoAsync(string id, TodoRequestModels.UpdateTodoRequest todo)
    {
        var existing = await _todoCollection.Find(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
        if (existing == null) return null;

        existing.Title = todo.Title;
        existing.Description = todo.Description;
        existing.DueDate = todo.DueDate;
        existing.IsCompleted = todo.IsCompleted;
        existing.UpdatedAt = DateTime.UtcNow;

        await _todoCollection.ReplaceOneAsync(t => t.Id == id, existing);
        return MapToResponse(existing);
    }

    public async Task<bool> DeleteTodoAsync(string id)
    {
        var result = await _todoCollection.DeleteOneAsync(t => t.Id == id);
        return result.DeletedCount > 0;
    }

    private static TodoResponseModels MapToResponse(Todo todo)
    {
        return new TodoResponseModels
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            DueDate = todo.DueDate,
            CreatedAt = todo.CreatedAt,
            UpdatedAt = todo.UpdatedAt,
            IsCompleted = todo.IsCompleted,
            IsDeleted = todo.IsDeleted
        };
    }
}
