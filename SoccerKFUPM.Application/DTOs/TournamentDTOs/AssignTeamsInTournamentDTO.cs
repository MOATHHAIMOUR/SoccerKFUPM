namespace SoccerKFUPM.Application.DTOs.TournamentDTOs;

public class AssignTeamsInTournamentDTO
{
    public int TournamentId { get; set; }
    public required List<int> TeamIds { get; set; }
}