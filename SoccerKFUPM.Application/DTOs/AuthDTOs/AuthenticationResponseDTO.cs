namespace SoccerKFUPM.Application.DTOs.AuthDTOs;

public class AuthenticationResponseDTO
{
    public int UserId { get; set; }
    public string AuthenticationMessage { get; set; }
    public string JWTToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime tokenExpirersAt { set; get; }
    public DateTime refreshTokenExpiration { get; set; }
    public bool IsValid { get; set; }

    public List<string> Roles = [];
}