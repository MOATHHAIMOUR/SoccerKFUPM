using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.DTOs.TournamentDTOs.Profile
{
    public class TournamentProfile : AutoMapper.Profile
    {
        public TournamentProfile()
        {
            CreateMap<AddTournamentDTO, Tournament>();
            CreateMap<UpdateTournamentDTO, Tournament>();
            CreateMap<Tournament, TournamentDTO>();

            CreateMap<Tournament, TournamentDTO>();
            CreateMap<AddTournamentDTO, Tournament>();

        }
    }


}
