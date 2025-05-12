﻿using SoccerKFUPM.Domain.Entities.Enums;

namespace SoccerKFUPM.Application.DTOs.MatchDTOs
{
    public class MatchSubstitutionDTO
    {
        public int PlayerInTeamId { get; set; }
        public int PlayerTeamOutId { get; set; }
        public int TimeMinute { get; set; }
        public int Reason { get; set; }
    }
}
