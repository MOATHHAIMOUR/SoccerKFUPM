using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ManagerDTOs;

namespace SoccerKFUPM.Application.Features.ManagersFeature.Commands.AddManager;

public record AddManagerCommand(AddManagerDTO dto) : IRequest<ApiResponse<bool>>
{
    public AddManagerDTO AddManagerDTO { get; set; } = dto;
}