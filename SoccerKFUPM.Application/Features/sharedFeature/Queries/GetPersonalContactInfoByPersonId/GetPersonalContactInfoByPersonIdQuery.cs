using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.ContactInfoDTOs;

namespace SoccerKFUPM.Application.Features.SharedFeature.Queries.GetPersonalContactInfoByPersonId;

public record GetPersonalContactInfoByPersonIdQuery(int PersonId) : IRequest<Result<List<ContactInfoDTO>>>;
