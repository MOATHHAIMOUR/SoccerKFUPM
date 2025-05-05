namespace SoccerKFUPM.Domain.Entities;

public class TeamContactInfo
{
    public int TeamContactInfoId { get; set; } // Primary Key
    public int TeamId { get; set; } // Foreign Key to Team
    public string ContactType { get; set; } = null!; // e.g., Email, Phone

    // Navigation property
    public Team Team { get; set; } = null!;
}