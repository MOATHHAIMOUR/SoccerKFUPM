using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
namespace SoccerKFUPM.Application.Services.IServises;

public interface IAuthenticationServices
{
    public Task<Result<AuthenticationResponseDTO>> AuthenticateUser(string userId, string password);
    public Task<Result<AuthenticationResponseDTO>> RegisterUserAsync(RegsterAccountRequestDTO registerUserDTO);
    public Task<Result<AuthenticationResponseDTO>> RefreshTokenAsync(string token);
    public Task<Result<List<RoleDTO>>> GetAllRolesAsync();

}