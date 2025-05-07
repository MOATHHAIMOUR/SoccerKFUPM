using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.CoachDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace SoccerKFUPM.Application.Services;

public class CoachServices : ICoachServices
{
    private readonly ICoachRepository _coachRepository;
    private readonly IMapper _mapper;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ITeamRepository _teamRepository;

    public CoachServices(ICoachRepository coachRepository, IMapper mapper, ITournamentRepository tournamentRepository, ITeamRepository teamRepository)
    {
        _coachRepository = coachRepository;
        _mapper = mapper;
        _tournamentRepository = tournamentRepository;
        _teamRepository = teamRepository;
    }

    public async Task<Result<bool>> AddCoachAsync(Coache coach)
    {

        var result = await _coachRepository.AddCoachAsync(coach);
        return Result<bool>.Success(result);

    }

    public async Task<Result<(List<CoachViewDTO> coaches, int totalCount)>> GetCoachesAsync(
      string? kfupmId = null,
      string? firstName = null,
      string? secondName = null,
      string? thirdName = null,
      string? lastName = null,
      DateTime? dateOfBirth = null,
      int? nationalityId = null,
      string? teamName = null,
      bool? isActive = null,
      int pageNumber = 1,
      int pageSize = 10)
    {
        var (coaches, totalCount) = await _coachRepository.GetCoachesAsync(
            kfupmId,
            firstName,
            secondName,
            thirdName,
            lastName,
            dateOfBirth,
            nationalityId,
            teamName,
            isActive,
            pageNumber,
            pageSize
        );

        var coachDTOs = coaches.Select(c => _mapper.Map<CoachViewDTO>(c)).ToList();

        return Result<(List<CoachViewDTO> coaches, int totalCount)>.Success((coachDTOs, totalCount));
    }

    public async Task<Result<bool>> AssignCoachToTeamAsync(CoachTeam coachTeam, int tournamentId)
    {
        // 1. Check tournament exists
        if (!await _tournamentRepository.TournamentExistsAsync(tournamentId))
        {
            return Result<bool>.Failure(Error.RecoredNotFound("Tournament not found"), HttpStatusCode.NotFound);
        }

        // 2. Check team exists
        if (!await _teamRepository.TeamExistsAsync(coachTeam.TeamId))
        {
            return Result<bool>.Failure(Error.RecoredNotFound("Team not found"), HttpStatusCode.NotFound);
        }


        // 3. Check if coach already assigned to another team in this tournament
        if (await _coachRepository.IsCoachAlreadyAssignedAsync(coachTeam.CoachId, tournamentId))
        {
            return Result<bool>.Failure(Error.ConflictError("Coach is already assigned to a team in this tournament"), HttpStatusCode.Conflict);
        }

        // 4. All validations passed → assign coach
        await _coachRepository.AssignCoachToTeamAsync(coachTeam);

        return Result<bool>.Success(true);
    }


}