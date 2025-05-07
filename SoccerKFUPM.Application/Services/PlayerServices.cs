using AutoMapper;
using Namespace.SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;
using System.Net;

namespace Namespace.SoccerKFUPM.Application.Services.PlayerServices;

public class PlayerServices : IPlayerServices
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;

    public PlayerServices(IPlayerRepository playerRepository, ITournamentRepository tournamentRepository, ITeamRepository teamRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _tournamentRepository = tournamentRepository;
        _teamRepository = teamRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> AddPlayerAsync(Player player)
    {
        bool result = await _playerRepository.AddPlayerAsync(player);

        return Result<bool>.Success(result);
    }

    public async Task<Result<(List<PlayerDTO> playerDTOs, int totalCount)>> GetAllPlayersAsync(
      int? playerId,
      string? kfupmId,
      int pageNumber = 1,
      int pageSize = 10)
    {
        (List<PlayerView> playerViews, int totalCount) = await _playerRepository.GetAllPlayersAsync(playerId, kfupmId, pageNumber, pageSize);

        var playerDTOs = _mapper.Map<List<PlayerDTO>>(playerViews);

        return Result<(List<PlayerDTO> playerDTOs, int totalCount)>.Success((playerDTOs, totalCount));
    }


    public async Task<Result<PlayerDTO>> GetPlayerByIdAsync(int playerId)
    {
        var player = await _playerRepository.GetPlayerByIdAsync(playerId);
        var playerDto = _mapper.Map<PlayerDTO>(player);
        if (player == null)
        {
            return Result<PlayerDTO>.Failure(Error.RecoredNotFound($"Player with id: {playerId} is not found"), System.Net.HttpStatusCode.NotFound);
        }

        return Result<PlayerDTO>.Success(playerDto);
    }

    public async Task<Result<bool>> AssignPlayerToTeamAsync(PlayerTeam playerTeam, int tournamentId)
    {
        // 1. Check if tournament exists
        if (!await _tournamentRepository.TournamentExistsAsync(tournamentId))
        {
            return Result<bool>.Failure(
                Error.RecoredNotFound("Tournament not found."),
                HttpStatusCode.NotFound);
        }

        // 2. Check if team exists
        if (!await _teamRepository.TeamExistsAsync(playerTeam.TeamId))
        {
            return Result<bool>.Failure(
                Error.RecoredNotFound("Team not found."),
                HttpStatusCode.NotFound);
        }

        // 3. Check if team is part of the tournament
        if (!await _teamRepository.IsTeamInTournamentAsync(playerTeam.TeamId, tournamentId))
        {
            return Result<bool>.Failure(
                Error.ValidationError("Team is not participating in the specified tournament."),
                HttpStatusCode.BadRequest);
        }

        // 4. Check if player is already assigned in this tournament
        if (await _playerRepository.IsPlayerAlreadyAssignedAsync(playerTeam.PlayerId, tournamentId))
        {
            return Result<bool>.Failure(
                Error.ConflictError("Player is already assigned to another team in this tournament."),
                HttpStatusCode.Conflict);
        }

        // 5. All checks passed — assign player
        await _playerRepository.AssignPlayerToTeamAsync(playerTeam);

        return Result<bool>.Success(true);
    }

}