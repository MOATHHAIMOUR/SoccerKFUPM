using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
namespace SoccerKFUPM.Application.DTOs.PlayerDTOs.Profile;
public class PlayerProfile : AutoMapper.Profile
{
    public PlayerProfile()
    {

        CreateMap<PlayerView, PlayerDTO>();

        CreateMap<AddPlayerDTO, Player>()
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new Person
            {
                KFUPMId = src.KFUPMId,
                FirstName = src.FirstName,
                SecondName = src.SecondName,
                ThirdName = src.ThirdName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                NationalityId = src.NationalityId ?? 0,
                PersonalContactInfos = src.PersonalContactInfos
                .Select(x => new PersonalContactInfo
                {
                    Value = x.Value,
                    ContactType = x.ContactType
                }).ToList()
            }));

        CreateMap<Player, PlayerDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth));
    }
}