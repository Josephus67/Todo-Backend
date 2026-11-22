using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Providers;
using Microsoft.AspNetCore.Authorization;
namespace TodoApp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class TodoController(IDataprovider _dataProvider) : ControllerBase
{
//get all todos
    [HttpGet(Name = "GetAllTodos")]
    public async Task<IActionResult> GetAllTodos()
    {
        var todos = await _dataProvider.GetAllTodosAsync();
        return Ok(todos);
    }

    //get todo by id
    [HttpGet("{id}", Name = "GetTodoById")]
    public async Task<IActionResult> GetTodoById(string id)
    {
        var todo = await _dataProvider.GetTodoByIdAsync(id);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    //create todo
    [HttpPost(Name = "CreateTodo")]
    public async Task<IActionResult> CreateTodo([FromBody] TodoRequestModels.CreateTodoRequest todo)
    {
        var createdTodo = await _dataProvider.CreateTodoAsync(todo);
        return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
    }

    //update todo
    [HttpPut("{id}", Name = "UpdateTodo")]
    public async Task<IActionResult> UpdateTodo(string id, [FromBody] TodoRequestModels.UpdateTodoRequest todo)
    {
        var updatedTodo = await _dataProvider.UpdateTodoAsync(id, todo);
        if (updatedTodo == null)
        {
            return NotFound();
        }
        return Ok(updatedTodo);
    }

    //delete todo
    [HttpDelete("{id}", Name = "DeleteTodo")]
    public async Task<IActionResult> DeleteTodo(string id)
    {
        var deleted = await _dataProvider.DeleteTodoAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok();
    }   



}
