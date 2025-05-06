namespace SoccerKFUPM.Application.DTOs.AuthDTOs;

public class AuthenticationResponseDTO
{
    public int UserId { get; set; }
    public string AuthenticationMessage { get; set; } = null!;
    public string JWTToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;

    public List<string> Roles = [];
}