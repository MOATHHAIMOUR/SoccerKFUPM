using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace SoccerKFUPM.Application.Services;

public class MatchServices : IMatchServices
{
    private readonly IMatchRepository _matchRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IFieldRepository _fieldRepository;
    private readonly IMapper _mapper;

    public MatchServices(
        IMatchRepository matchRepository,
        ITournamentRepository tournamentRepository,
        ITeamRepository teamRepository,
        IFieldRepository fieldRepository,
        IMapper mapper)
    {
        _matchRepository = matchRepository;
        _tournamentRepository = tournamentRepository;
        _teamRepository = teamRepository;
        _fieldRepository = fieldRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> ScheduleMatchAsync(MatchSchedule match)
    {
        // 1. Validate tournament exists
        var tournamentExists = await _tournamentRepository.TournamentExistsAsync(match.TournamentId);

        if (!tournamentExists)
        {
            return Result<bool>.Failure(
                Error.RecoredNotFound($"Tournament with id: {match.TournamentId} is not found"),
                HttpStatusCode.NotFound);
        }



        var teamAInTournament = await _teamRepository.IsTeamInTournamentAsync(match.TournamentTeamIdA);
        if (!teamAInTournament)
        {
            return Result<bool>.Failure(
                Error.ValidationError($"Team A with id: {match.TournamentTeamIdA} is not in tournament {match.TournamentId}"),
                HttpStatusCode.BadRequest);
        }

        var teamBInTournament = await _teamRepository.IsTeamInTournamentAsync(match.TournamentTeamIdB);
        if (!teamBInTournament)
        {
            return Result<bool>.Failure(
                Error.ValidationError($"Team B with id: {match.TournamentTeamIdB} is not in tournament {match.TournamentId}"),
                HttpStatusCode.BadRequest);
        }

        // 3. Schedule the match
        var result = await _matchRepository.ScheduleMatchAsync(match);
        return Result<bool>.Success(result);
    }

    public async Task<Result<(List<MatchView> matches, int totalCount)>> GetAllScheduledMatchesAsync(
     int? tournamentId = null,
     int? tournamentPhase = null,
     string? teamAName = null,
     string? teamBName = null,
     string? fieldName = null,
     DateTime? matchDate = null,
     int pageNumber = 1,
     int pageSize = 10)
    {
        var (matches, totalCount) = await _matchRepository.SearchMatchesAsync(
            tournamentId, tournamentPhase, teamAName, teamBName, fieldName, matchDate, pageNumber, pageSize);

        return Result<(List<MatchView> matches, int totalCount)>.Success((matches, totalCount));
    }

}