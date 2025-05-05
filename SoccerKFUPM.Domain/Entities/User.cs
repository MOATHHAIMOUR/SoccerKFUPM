using Microsoft.AspNetCore.Identity;

namespace SoccerKFUPM.Domain.Entities;

public class User : IdentityUser<int>
{
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
}