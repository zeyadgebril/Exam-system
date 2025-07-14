using AutoMapper;
using Backend.DTOs;
using Backend.Repository.Auth;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IMapper _mapper;
        public AuthService(IAuthRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<UserDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _repo.AuthenticateAsync(dto);
            if (user == null) throw new UnauthorizedAccessException("Invalid credentials");

            var token = await _repo.GenerateJwtTokenAsync(user);
            var result = _mapper.Map<UserDTO>(user);
            result.Token = token;
            return result;
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO dto)
        {
            var user = await _repo.RegisterAsync(dto, dto.Password);
            var token = await _repo.GenerateJwtTokenAsync(user);

            var result = _mapper.Map<UserDTO>(user);
            result.Token = token;
            return result;
        }
    }
}
