using AutoMapper;
using SoccerKFUPM.Application.DTOs.SharedDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Common.Mapping;

public class FieldProfile : Profile
{
    public FieldProfile()
    {
        CreateMap<Field, FieldDTO>();
    }
}