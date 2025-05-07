using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.SharedDTOs;

namespace SoccerKFUPM.Application.Features.FieldFeature.Queries.GetAllFields;

public record GetAllFieldsQuery() : IRequest<ApiResponse<List<FieldDTO>>>;