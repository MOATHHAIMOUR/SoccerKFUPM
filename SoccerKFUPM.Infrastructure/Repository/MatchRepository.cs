using Microsoft.Data.SqlClient;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
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
                PhaseName = reader.GetString(reader.GetOrdinal("PhaseName")),
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

    public async Task<bool> MatchScheduleExistsAsync(int matchId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SELECT COUNT(1) FROM MatchSchedules WHERE MatchScheduleId = @MatchId", connection);
        command.Parameters.AddWithValue("@MatchId", matchId);

        await connection.OpenAsync();
        var count = (int)await command.ExecuteScalarAsync();
        return count > 0;
    }


    public async Task<int?> InsertMatchRecordAsync(MatchRecord record)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_InsertMatchRecord", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@MatchScheduleId", record.MatchScheduleId);
        command.Parameters.AddWithValue("@TournamentTeamId", record.TournamentTeamId);
        command.Parameters.AddWithValue("@GoalsFor", record.GoalsFor);
        command.Parameters.AddWithValue("@GoalAgainst", record.GoalAgainst);
        command.Parameters.AddWithValue("@AcquisitionRate", record.AcquisitionRate);
        command.Parameters.AddWithValue("@IsWin", record.IsWin);
        command.Parameters.AddWithValue("@BestPlayer", (object?)record.BestPlayer ?? DBNull.Value);

        var outputId = new SqlParameter("@InsertedMatchRecoredId", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        command.Parameters.Add(outputId);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        return outputId.Value as int?;
    }


    public async Task<MatchSchedule?> GetMatchScheduleByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_GetMatchScheduleById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@MatcheScheduleId", id);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return new MatchSchedule
        {
            MatchScheduleId = reader.GetInt32(reader.GetOrdinal("MatcheScheduleId")),
            TournamentId = reader.GetInt32(reader.GetOrdinal("TournamentId")),
            TournamentPhase = (TournamentPhase)reader.GetInt32(reader.GetOrdinal("TournamentPhase")),
            Number = reader.GetInt32(reader.GetOrdinal("Number")),
            TournamentTeamIdA = reader.GetInt32(reader.GetOrdinal("TournamentTeamIdA")),
            TournamentTeamIdB = reader.GetInt32(reader.GetOrdinal("TournamentTeamIdB")),
            Date = reader.GetDateTime(reader.GetOrdinal("Date")),
            MatchStatus = (MatchStatus)reader.GetInt32(reader.GetOrdinal("Status")),
            FieldId = reader.GetInt32(reader.GetOrdinal("FieldId"))
        };
    }

    public async Task<bool> ValidatePlayersInTeamInTournamentAsync(int tournamentId, int tournamentTeamId, List<int> playerIds)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_ValidatePlayersInTeamInTournament", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@TournamentId", tournamentId);
        command.Parameters.AddWithValue("@TournamentTeamId", tournamentTeamId);

        // Prepare the TVP
        var tvp = new DataTable();
        tvp.Columns.Add("PlayerId", typeof(int));
        foreach (var id in playerIds)
            tvp.Rows.Add(id);

        var playerIdsParam = command.Parameters.AddWithValue("@PlayerIds", tvp);
        playerIdsParam.SqlDbType = SqlDbType.Structured;
        playerIdsParam.TypeName = "PlayerIdList";

        await connection.OpenAsync();
        var returnValue = new SqlParameter
        {
            Direction = ParameterDirection.ReturnValue
        };
        command.Parameters.Add(returnValue);

        await command.ExecuteNonQueryAsync();

        return (int)returnValue.Value == 1;
    }


    public async Task<bool> ValidateTournamentRefereesAsync(
    int matchScheduleId,
    List<int> tournamentRefereeIds)
    {
        var table = new DataTable();
        table.Columns.Add("TournamentRefereeId", typeof(int));

        foreach (var id in tournamentRefereeIds)
            table.Rows.Add(id);

        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_ValidateTournamentRefereesInMatch", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@MatchScheduleId", matchScheduleId);

        var tvpParam = command.Parameters.AddWithValue("@TournamentRefereeIds", table);
        tvpParam.SqlDbType = SqlDbType.Structured;
        tvpParam.TypeName = "TournamentRefereeIdList";

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();
        return result != null && Convert.ToBoolean(result);
    }


    public async Task InsertShotsOnGoalAsync(List<ShotOnGoal> shots)
    {
        var table = new DataTable();
        table.Columns.Add("MatchRecordId", typeof(int));
        table.Columns.Add("Time", typeof(int));
        table.Columns.Add("PlayerTeamId", typeof(int));
        table.Columns.Add("GoalkeeperTeamId", typeof(int));
        table.Columns.Add("ShotType", typeof(int));
        table.Columns.Add("IsGoal", typeof(bool));

        foreach (var shot in shots)
        {
            table.Rows.Add(
                shot.MatchRecoredId,
                shot.Time,
                shot.PlayerTeamId,
                shot.GoalkeeperTeamId,
                shot.ShotType,
                shot.IsGoal
            );
        }

        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_InsertShotsOnGoal", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        var tvpParam = command.Parameters.AddWithValue("@Shots", table);
        tvpParam.SqlDbType = SqlDbType.Structured;
        tvpParam.TypeName = "ShotOnGoalList";

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Match>> GetUpcomingMatchesByTeamAsync(
       string? teamName = null,
       string? tournamentName = null,
       DateTime? fromDate = null,
       DateTime? toDate = null,
       int pageNumber = 1,
       int pageSize = 10)
    {
        var matches = new List<Match>();

        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_GetUpcomingMatchesByTeam", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@TeamName", (object?)teamName ?? DBNull.Value);
        command.Parameters.AddWithValue("@TournamentName", (object?)tournamentName ?? DBNull.Value);
        command.Parameters.AddWithValue("@FromDate", (object?)fromDate ?? DBNull.Value);
        command.Parameters.AddWithValue("@ToDate", (object?)toDate ?? DBNull.Value);
        command.Parameters.AddWithValue("@PageNumber", pageNumber);
        command.Parameters.AddWithValue("@PageSize", pageSize);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var match = new Match
            {
                MatchScheduleId = reader.GetInt32(reader.GetOrdinal("MatchScheduleId")),
                TeamAName = reader.GetString(reader.GetOrdinal("TeamAName")),
                TeamBName = reader.GetString(reader.GetOrdinal("TeamBName")),
                MatchDate = reader.GetDateTime(reader.GetOrdinal("MatchDate")),
                TournamentName = reader.GetString(reader.GetOrdinal("TournamentName")),
            };

            matches.Add(match);
        }

        return matches;
    }

}