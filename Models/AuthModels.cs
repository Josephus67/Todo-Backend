namespace TodoApp.Models;
public class RegisterRequestModel
{
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
}
public class LoginRequestModel
{
    public string Email {get; set;} = string.Empty;
    public string Password {get; set; } = string.Empty;

}

public class AuthResponseModel
{
    public string Token {get; set;} = string.Empty;
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string Message {get; set;} = string.Empty;
}