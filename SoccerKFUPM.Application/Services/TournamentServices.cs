using AutoMapper;
using SoccerKFUPM.Application.Common.Errors;
using SoccerKFUPM.Application.Common.ResultPattern;
using SoccerKFUPM.Application.DTOs.TournamentDTOs;
using SoccerKFUPM.Application.Services.IServises;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.IRepository;

namespace SoccerKFUPM.Application.Services;

public class TournamentServices : ITournamentServices
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMapper _mapper;

    public TournamentServices(ITournamentRepository tournamentRepository, IMapper mapper)
    {
        _tournamentRepository = tournamentRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> AddTournamentAsync(Tournament tournament)
    {
        int insertedId = await _tournamentRepository.AddTournamentAsync(tournament);
        return Result<bool>.Success(insertedId > 0);
    }

    public async Task<Result<bool>> DeleteTournamentAsync(int tournamentId)
    {
        bool result = await _tournamentRepository.DeleteTournamentAsync(tournamentId);
        if (!result)
        {
            return Result<bool>.Failure(Error.RecoredNotFound($"Tournament with id: {tournamentId}"), System.Net.HttpStatusCode.NotFound);
        }
        return Result<bool>.Success(result);
    }
    public async Task<Result<(List<TournamentDTO> tournaments, int totalCount)>> GetAllTournamentsAsync(
        string? number = null,
        string? name = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var (tournaments, totalCount) = await _tournamentRepository.SearchTournamentsAsync(
            number: number,
            name: name,
            startDate: startDate,
            endDate: endDate,
            pageNumber: pageNumber,
            pageSize: pageSize
        );

        var tournamentDTOs = _mapper.Map<List<TournamentDTO>>(tournaments);
        return Result<(List<TournamentDTO> tournaments, int totalCount)>.Success((tournamentDTOs, totalCount));
    }

    public async Task<Result<TournamentDTO>> GetTournamentByIdAsync(int tournamentId)
    {
        var tournament = await _tournamentRepository.GetTournamentByIdAsync(tournamentId);
        if (tournament == null)
        {
            return Result<TournamentDTO>.Failure(Error.RecoredNotFound($"Tournament with id: {tournamentId}"), System.Net.HttpStatusCode.NotFound);
        }
        var tournamentDTO = _mapper.Map<TournamentDTO>(tournament);
        return Result<TournamentDTO>.Success(tournamentDTO);
    }

    public async Task<Result<bool>> UpdateTournamentAsync(Tournament tournament)
    {
        var exists = await _tournamentRepository.GetTournamentByIdAsync(tournament.TournamentId);

        if (exists == null)
        {
            return Result<bool>.Failure(Error.RecoredNotFound($"Tournament with id: {tournament.TournamentId}"), System.Net.HttpStatusCode.NotFound);
        }

        bool result = await _tournamentRepository.UpdateTournamentAsync(tournament);
        return Result<bool>.Success(result);
    }
}