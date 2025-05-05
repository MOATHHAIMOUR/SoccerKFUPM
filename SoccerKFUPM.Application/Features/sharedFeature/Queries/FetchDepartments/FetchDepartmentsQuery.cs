using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.SharedDTOs;

namespace SoccerKFUPM.Application.Features.sharedFeature.Queries.FetchDepartments;

public class FetchDepartmentsQuery : IRequest<ApiResponse<List<DepartmentDTO>>> { }