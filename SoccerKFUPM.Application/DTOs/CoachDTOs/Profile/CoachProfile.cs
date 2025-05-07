using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Entities.Views;

namespace SoccerKFUPM.Application.DTOs.CoachDTOs.Profile;

public class CoachProfile : AutoMapper.Profile
{
    public CoachProfile()
    {
        CreateMap<AddCoachDTO, Coache>()
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => new Person
            {
                KFUPMId = src.KFUPMId,
                FirstName = src.FirstName,
                SecondName = src.SecondName,
                ThirdName = src.ThirdName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                NationalityId = src.NationalityId,
                PersonalContactInfos = src.PersonalContactInfos.Select(c => new PersonalContactInfo
                {
                    ContactType = (ContactType)c.ContactType,
                    Value = c.Value
                }).ToList()
            }));

        CreateMap<CoachView, CoachViewDTO>();

        CreateMap<Coache, CoachDTO>()
            .ForMember(dest => dest.KFUPMId, opt => opt.MapFrom(src => src.Person.KFUPMId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
            .ForMember(dest => dest.SecondName, opt => opt.MapFrom(src => src.Person.SecondName))
            .ForMember(dest => dest.ThirdName, opt => opt.MapFrom(src => src.Person.ThirdName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Person.DateOfBirth))
            .ForMember(dest => dest.NationalityId, opt => opt.MapFrom(src => src.Person.NationalityId))
            .ForMember(dest => dest.PersonalContactInfos, opt => opt.MapFrom(src => src.Person.PersonalContactInfos.Select(c => new PersonalContactInfoDTO
            {
                ContactType = (int)c.ContactType,
                Value = c.Value
            }).ToList()));
    }
}