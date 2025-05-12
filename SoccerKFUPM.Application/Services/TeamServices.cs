using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TeamDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.IRepository;

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

    public async Task<Result<TeamViewDTO>> GetTeamByIdAsync(int teamId)
    {
        var team = await _teamRepository.GetTeamByIdAsync(teamId);
        if (team == null)
        {
            return Result<TeamViewDTO>.Failure(Error.RecoredNotFound($"Team with id: {teamId} is not found"), System.Net.HttpStatusCode.NotFound);
        }
        var teamDto = _mapper.Map<TeamViewDTO>(team);

        return Result<TeamViewDTO>.Success(teamDto);

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

    public async Task<Result<(List<TeamTournamentViewDTO> teams, int totalCount)>> GetTeamsByTournamentAsync(
        int tournamentId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var tournamentExists = await _tournamentRepository.TournamentExistsAsync(tournamentId);
        if (!tournamentExists)
        {
            return Result<(List<TeamTournamentViewDTO> teams, int totalCount)>.Failure(
                Error.RecoredNotFound($"Tournament with id: {tournamentId} is not found"),
                System.Net.HttpStatusCode.NotFound);
        }

        var (teams, totalCount) = await _teamRepository.GetTeamsByTournamentIdAsync(tournamentId, pageNumber, pageSize);

        var teamDTOs = _mapper.Map<List<TeamTournamentViewDTO>>(teams);

        return Result<(List<TeamTournamentViewDTO> teams, int totalCount)>.Success((teamDTOs, totalCount));
    }
}