using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.CoachDTOs;

namespace SoccerKFUPM.Application.Features.CoachFeature.Commands.AddCoach;

public record AddCoachCommand(AddCoachDTO Dto) : IRequest<ApiResponse<bool>>;