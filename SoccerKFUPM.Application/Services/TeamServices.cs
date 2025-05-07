using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace SoccerKFUPM.Application.Services;

public class TeamServices : ITeamServices
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMapper _mapper;

    public TeamServices(ITeamRepository teamRepository, ITournamentRepository tournamentRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _tournamentRepository = tournamentRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> AddTeamAsync(Team team)
    {
        bool result = await _teamRepository.AddTeamAsync(team);
        return Result<bool>.Success(result);
    }

    public async Task<Result<bool>> DeleteTeamAsync(int teamId)
    {
        bool result = await _teamRepository.DeleteTeamAsync(teamId);
        if (!result)
        {
            return Result<bool>.Failure(Error.RecoredNotFound($"Team with id: {teamId}"), System.Net.HttpStatusCode.NotFound);
        }
        return Result<bool>.Success(result);
    }

    public async Task<Result<(List<TeamDTO> teams, int totalCount)>> SearchTeamsAsync(
        string? name = null,
        string? address = null,
        string? website = null,
        int? numberOfPlayers = null,
        int? managerId = null,
        string? managerFirstName = null,
        string? managerLastName = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var (teams, totalCount) = await _teamRepository.SearchTeamsAsync(
            name, address, website, numberOfPlayers,
            managerId, managerFirstName, managerLastName,
            pageNumber, pageSize);

        var teamDTOs = _mapper.Map<List<TeamDTO>>(teams);

        return Result<(List<TeamDTO> teams, int totalCount)>.Success((teamDTOs, totalCount));
    }

    public async Task<Result<TeamDTO>> GetTeamByIdAsync(int teamId)
    {
        var team = await _teamRepository.GetTeamByIdAsync(teamId);
        if (team == null)
        {
            return Result<TeamDTO>.Failure(Error.RecoredNotFound($"Team with id: {teamId} is not found"), System.Net.HttpStatusCode.NotFound);
        }
        var teamDto = _mapper.Map<TeamDTO>(team);
        return Result<TeamDTO>.Success(teamDto);
    }

    public async Task<Result<bool>> UpdateTeamAsync(Team team)
    {
        bool result = await _teamRepository.UpdateTeamAsync(team);
        if (!result)
        {
            return Result<bool>.Failure(Error.RecoredNotFound($"Team with id: {team.TeamId} is not found"), System.Net.HttpStatusCode.NotFound);
        }
        return Result<bool>.Success(result);
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

        // 3. Check team is in tournament
        if (!await _teamRepository.IsTeamInTournamentAsync(coachTeam.TeamId, tournamentId))
        {
            return Result<bool>.Failure(Error.ValidationError("Team is not part of the tournament"), HttpStatusCode.BadRequest);
        }

        // 4. Check if coach already assigned to another team in this tournament
        if (await _teamRepository.IsCoachAlreadyAssignedAsync(coachTeam.CoachId, tournamentId))
        {
            return Result<bool>.Failure(Error.ConflictError("Coach is already assigned to a team in this tournament"), HttpStatusCode.Conflict);
        }

        // 5. All validations passed → assign coach
        await _teamRepository.AssignCoachToTeamAsync(coachTeam);

        return Result<bool>.Success(true);
    }



}