using MediatR;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.PlayerDTOs;

namespace SoccerKFUPM.Application.Features.PlayerFeature.Queries.FetchPlayers;

public class FetchPlayersQuery : IRequest<ApiResponse<List<PlayerDTO>>> {

    public int? PlayerId { get; set; } = null;
    public  string? KfupmId { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;


public FetchPlayersQuery(int? playerId, string? kfupmId, int pageNumber, int pageSize) {
    PlayerId = playerId;
    KfupmId = kfupmId;
    PageNumber = pageNumber;
    PageSize = pageSize;   
}



}