using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Commands.AddPlayer;

public class AddPlayerCommand(AddPlayerDTO addPlayerDTO) : IRequest<ApiResponse<bool>>
{
    public AddPlayerDTO AddPlayerDTO { get; set; } = addPlayerDTO;
}