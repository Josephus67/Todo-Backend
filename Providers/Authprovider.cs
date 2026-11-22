using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using TodoApp.Configurations;
using TodoApp.Models;
namespace TodoApp.Providers;
public class AuthProvider(IMongoCollection<User> userCollection, JwtSettings jwtSettings) : IAuthProvider
{
    public async Task<AuthResponseModel?> RegisterAsync(RegisterRequestModel model)
    {
        var existingUser = await userCollection.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var user = new User
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Username = model.Username,
            Email = model.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            CreatedAt = DateTime.UtcNow
        };
        await userCollection.InsertOneAsync(user);

        return new AuthResponseModel
        {
            Username = user.Username,
            Email = user.Email,
            Message = "User registered successfully. Login to get your token."
        };


    }

    public async Task<AuthResponseModel?> LoginAsync(LoginRequestModel model)
    {
        var user = await userCollection.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
        {
            throw new Exception("Invalid email or password.");
        }

        var token = GenerateJwtToken(user);

        return new AuthResponseModel
        {
            Token = token,
            Username = user.Username,
            Email = user.Email,
            Message = "User logged in successfully."
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}