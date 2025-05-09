using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.MatchDTOs
{
    public class MatchSubstitutionDTO
    {
        public int MatchRecordId { get; set; }
        public int PlayerInTeamId { get; set; }
        public int PlayerTeamOutId { get; set; }
        public int TimeMinute { get; set; }
        public SubstitutionReason Reason { get; set; }
    }
}
