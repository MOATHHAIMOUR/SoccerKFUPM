﻿using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.DTOs.TeamDTOs.Profiler
{
    public class TeamProfiler : AutoMapper.Profile
    {
        public TeamProfiler()
        {

            CreateMap<UpdateTeamDTO, Team>();
            CreateMap<Team, TeamDTO>();

            CreateMap<TeamView, TeamDTO>();

            CreateMap<ContactInfoDTOs.ContactInfoDTO, TeamContactInfo>()
                .ForMember(dest => dest.ContactType, opt => opt.MapFrom(src => (ContactType)src.ContactType));

            CreateMap<AddTeamDTO, Team>()
                .ForMember(dest => dest.TeamContactInfo, opt => opt.MapFrom(src => src.ContactInfo));


            CreateMap<TeamView, TeamViewDTO>()
                .ForMember(dest => dest.contactInfoDTOs, opt => opt.MapFrom(src => src.teamContactInfos));

            CreateMap<TeamTournamentView, TeamTournamentViewDTO>();

        }
    }
}
