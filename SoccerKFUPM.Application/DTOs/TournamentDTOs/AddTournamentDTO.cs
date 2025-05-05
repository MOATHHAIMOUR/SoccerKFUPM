namespace SoccerKFUPM.Application.DTOs.TournamentDTOs;

public class AddTournamentDTO
{
    public required string Number { get; set; }
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
}