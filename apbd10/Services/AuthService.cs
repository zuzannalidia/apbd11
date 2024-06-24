using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using apbd10.Data;
using apbd10.DTO;
using apbd10.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace apbd10.Services;

public class AuthService : IAuthService
{
    private readonly DatabaseContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(DatabaseContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterDTO registerDTO)
    {
        try
        {
            var user = new User
            {
                Login = registerDTO.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                RefreshToken = GenerateRefreshToken()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }
    }

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Login == loginDTO.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
        {
            throw new Exception("Invalid credentials.");
        }

        var token = GenerateJwtToken(user);
        user.RefreshToken = GenerateRefreshToken();
        await _context.SaveChangesAsync();
        return token;
    }

    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user == null)
        {
            throw new Exception("Invalid refresh token.");
        }

        var newJwtToken = GenerateJwtToken(user);
        user.RefreshToken = GenerateRefreshToken();
        await _context.SaveChangesAsync();

        return newJwtToken;
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
