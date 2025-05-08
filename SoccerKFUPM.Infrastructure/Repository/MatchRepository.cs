using Microsoft.Data.SqlClient;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;
using System.Data;

namespace SoccerKFUPM.Infrastructure.Repository;

public class MatchRepository : IMatchRepository
{
    private readonly IDbConnection _connection;

    public MatchRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> ScheduleMatchAsync(MatchSchedule match)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_ScheduleMatch", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@TournamentId", match.TournamentId);
        command.Parameters.AddWithValue("@TournamentPhase", (int)match.TournamentPhase);
        command.Parameters.AddWithValue("@TournamentTeamIdA", match.TournamentTeamIdA);
        command.Parameters.AddWithValue("@TournamentTeamIdB", match.TournamentTeamIdB);
        command.Parameters.AddWithValue("@Date", match.Date);
        command.Parameters.AddWithValue("@FieldId", match.FieldId);

        await connection.OpenAsync();
        var result = await command.ExecuteNonQueryAsync();
        return result > 0;
    }



    public async Task<(List<MatchView> Matches, int TotalCount)> SearchMatchesAsync(
    int? tournamentId = null,
    int? tournamentPhase = null,
    string? teamAName = null,
    string? teamBName = null,
    string? fieldName = null,
    DateTime? matchDate = null,
    int pageNumber = 1,
    int pageSize = 10)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_SearchMatches", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@TournamentId", tournamentId ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@TournamentPhase", tournamentPhase ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@TeamAName", (object?)teamAName ?? DBNull.Value);
        command.Parameters.AddWithValue("@TeamBName", (object?)teamBName ?? DBNull.Value);
        command.Parameters.AddWithValue("@FieldName", (object?)fieldName ?? DBNull.Value);
        command.Parameters.AddWithValue("@MatchDate", matchDate ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@PageNumber", pageNumber);
        command.Parameters.AddWithValue("@PageSize", pageSize);



        await connection.OpenAsync();
        var matches = new List<MatchView>();

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {

            matches.Add(new MatchView
            {
                MatchScheduleId = reader.GetInt32(reader.GetOrdinal("MatchScheduleId")),
                TournamentId = reader.GetInt32(reader.GetOrdinal("TournamentId")),
                TournamentName = reader.GetString(reader.GetOrdinal("TournamentName")),
                TournamentPhase = reader.GetInt32(reader.GetOrdinal("TournamentPhase")),
                PhaseName = reader.GetString(reader.GetOrdinal("PhaseName")), // ✅ correct
                Number = reader.GetString(reader.GetOrdinal("MatchNumber")),
                Date = reader.GetDateTime(reader.GetOrdinal("MatchDateTime")),
                TeamAName = reader.GetString(reader.GetOrdinal("TeamAName")),
                TeamBName = reader.GetString(reader.GetOrdinal("TeamBName")),
                FieldName = reader.GetString(reader.GetOrdinal("FieldName"))
            });
        }

        await reader.CloseAsync();
        return (matches, matches.Count);
    }


}