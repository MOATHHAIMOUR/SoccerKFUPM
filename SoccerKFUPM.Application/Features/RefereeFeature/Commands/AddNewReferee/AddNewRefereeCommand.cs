using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.RefereeDTOs;

namespace SoccerKFUPM.Application.Features.RefereeFeature.Commands.AddNewReferee;

public record AddNewRefereeCommand(AddRefereeDTO RefereeDTO) : IRequest<ApiResponse<bool>>;
