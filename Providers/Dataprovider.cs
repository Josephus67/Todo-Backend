using MongoDB.Bson;
using MongoDB.Driver;
using TodoApp.Models;

namespace TodoApp.Providers;
public class Dataprovider(IMongoCollection<Todo> _todoCollection) : IDataprovider
{
    public async Task<TodoResponseModels> CreateTodoAsync(TodoRequestModels.CreateTodoRequest todo)
    {
        Todo.Id = ObjectId.GenerateNewId().ToString();
        Todo.CreatedAt = DateTime.UtcNow;
        Todo.Title = todo.Title;
        Todo.Description = todo.Description;
        Todo.DueDate = todo.DueDate;
        Todo.IsCompleted = false;
        Todo.IsDeleted = false;
        await _todoCollection.InsertOneAsync(Todo);
        return Todo;
    }

    public async Task<List<TodoResponseModels>> GetAllTodosAsync(TodoRequestModels.GetAllTodosRequest todo)
    {
        return await _todoCollection.Find(t => !t.IsDeleted).ToListAsync();
    }

    public async Task<TodoResponseModels> GetTodoByIdAsync(string id)
    {
        return await _todoCollection.Find(t => t.Id == id && !t.IsDeleted).FirstOrDefaultAsync();
    }

    public async Task<TodoResponseModels> UpdateTodoAsync(string Id, TodoRequestModels.UpdateTodoRequest todo)
    {
        Todo.Id = Id;
        Todo.UpdatedAt = DateTime.UtcNow;
        Todo.Title = todo.Title;
        Todo.Description = todo.Description;
        Todo.DueDate = todo.DueDate;
        Todo.IsCompleted = todo.IsCompleted;
        Todo.IsDeleted = todo.IsDeleted;
        await _todoCollection.ReplaceOneAsync(t => t.Id == Id, Todo);
        return Todo;
    }

    public async Task<bool> DeleteTodoAsync(string id)
    {
        var result = await _todoCollection.DeleteOneAsync(t => t.Id == id);
        return result.DeletedCount > 0;
    }
}