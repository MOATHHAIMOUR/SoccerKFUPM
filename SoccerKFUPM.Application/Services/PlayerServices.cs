using AutoMapper;
using Namespace.SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;

namespace Namespace.SoccerKFUPM.Application.Services.PlayerServices;

public class PlayerServices : IPlayerServices
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public PlayerServices(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
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
            return Result<PlayerDTO>.Failure(Error.RecoredNotFound($"Player with id: {playerId}"));
        }

        return Result<PlayerDTO>.Success(playerDto);
    }
}