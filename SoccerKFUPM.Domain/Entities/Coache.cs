namespace SoccerKFUPM.Domain.Entities;

public class Coache
{
    public int CoachId { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;

}
