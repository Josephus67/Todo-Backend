using TodoApp.Models;
namespace TodoApp.Providers;
public interface IAuthProvider
{
    Task<AuthResponseModel?> RegisterAsync(RegisterRequestModel model);
    Task<AuthResponseModel?> LoginAsync(LoginRequestModel model);
}