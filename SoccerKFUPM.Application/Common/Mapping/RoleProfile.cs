using AutoMapper;
using SoccerKFUPM.Application.DTOs.AuthDTOs;
using SoccerKFUPM.Domain.Entities;

namespace SoccerKFUPM.Application.Common.Mapping;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, RoleDTO>();
    }
}