using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.MatchDTOs;

namespace SoccerKFUPM.Application.Features.MatchFeature.Commands.AddMatchResult;

public record AddMatchResultCommand(AddMatchRecordDTO Dto) : IRequest<ApiResponse<bool>>;