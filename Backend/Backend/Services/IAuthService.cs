using Backend.DTOs;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO dto);
        Task<UserDTO> LoginAsync(LoginDTO dto);
    }
}
