namespace SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Enums;

public class Player
{
    public int PlayerId { get; set; }
    public int PersonId { get; set; }
    public PlayerType PlayerType { get; set; } // Student, Faculty, or Staff
    public int DepartmentId { get; set; }
    public PlayerStatus PlayerStatus { get; set; }
    public PlayerPosition PlayerPosition { get; set; }
    public Person Person { get; set; } = null!;
}