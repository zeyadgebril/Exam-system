using Backend.DTOs;
using Backend.Models;

namespace Backend.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> RegisterAsync(RegisterDTO dto, string password);
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        Task<ApplicationUser> AuthenticateAsync(LoginDTO dto);
    }
}
