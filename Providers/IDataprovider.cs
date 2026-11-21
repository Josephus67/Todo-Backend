using TodoApp.Models;
namespace TodoApp.Providers;
public interface IDataprovider
{
    Task<TodoResponseModels> CreateTodoAsync(TodoRequestModels.CreateTodoRequest todo);
    Task<List<TodoResponseModels>> GetAllTodosAsync();
    Task<TodoResponseModels?> GetTodoByIdAsync(string id);
    Task<TodoResponseModels?> UpdateTodoAsync(string id, TodoRequestModels.UpdateTodoRequest todo);
    Task<bool> DeleteTodoAsync(string id);
}