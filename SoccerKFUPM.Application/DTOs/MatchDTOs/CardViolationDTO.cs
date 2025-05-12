using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.MatchDTOs
{
    public class CardViolationDTO
    {
        public int MatchRecordId { get; set; }
        public TimeSpan Time { get; set; }
        public int PlayerId { get; set; }
        public int InjuredPlayerTeamId { get; set; }
        public int TournamentRefreeId { get; set; }
        public CardType CardType { get; set; }
        public string? Notes { get; set; }
    }
}
