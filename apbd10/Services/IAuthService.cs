using apbd10.DTO;

namespace apbd10.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDTO registerDTO);
    Task<string> LoginAsync(LoginDTO loginDTO);
    Task<string> RefreshTokenAsync(string refreshToken);
}
