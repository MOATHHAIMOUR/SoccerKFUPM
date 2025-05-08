using Microsoft.Data.SqlClient;
using Namespace.SoccerKFUPM.Domain.IRepository;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.Enums;
using System.Data;


namespace SoccerKFUPM.Infrastructure.Repository;

public class PlayerRepository(IDbConnection connection) : IPlayerRepository
{
    private readonly IDbConnection _connection = connection;

    public async Task<bool> AddPlayerAsync(Player player)
    {
        using var command = new SqlCommand("dbo.SP_InsertPlayerWithMultipleContacts", (SqlConnection)_connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@KFUPMId", player.Person.KFUPMId);
        command.Parameters.AddWithValue("@FirstName", player.Person.FirstName);
        command.Parameters.AddWithValue("@SecondName", player.Person.SecondName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@ThirdName", player.Person.ThirdName ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@LastName", player.Person.LastName);
        command.Parameters.AddWithValue("@DateOfBirth", player.Person.DateOfBirth);
        command.Parameters.AddWithValue("@NationalityId", player.Person.NationalityId);
        command.Parameters.AddWithValue("@PlayerType", player.PlayerType);
        command.Parameters.AddWithValue("@DepartmentId", player.DepartmentId);
        command.Parameters.AddWithValue("@PlayerStatus", player.PlayerType);

        var contactTable = new DataTable();
        contactTable.Columns.Add("ContactType", typeof(string));
        contactTable.Columns.Add("Value", typeof(string));

        foreach (var contact in player.Person.PersonalContactInfos)
        {
            contactTable.Rows.Add((int)contact.ContactType, contact.Value);
        }

        var contactInfosParam = command.Parameters.AddWithValue("@ContactInfos", contactTable);
        contactInfosParam.SqlDbType = SqlDbType.Structured;
        contactInfosParam.TypeName = "dbo.ContactInfoType";

        if (_connection.State != ConnectionState.Open)
            await ((SqlConnection)_connection).OpenAsync();

        try
        {
            await command.ExecuteNonQueryAsync();
            return true;
        }
        catch
        {
            // optionally log the exception here
            throw;
        }
    }

    public async Task<Player?> GetPlayerByIdAsync(int playerId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        await connection.OpenAsync();

        var query = "SELECT * FROM Players WHERE PlayerId = @PlayerId";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@PlayerId", playerId);

        using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Player
            {
                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                PlayerType = (PlayerType)reader.GetInt32(reader.GetOrdinal("PlayerType")),
                PlayerPosition = (PlayerPosition)reader.GetInt32(reader.GetOrdinal("PlayerPosition")),
                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                PlayerStatus = (PlayerStatus)reader.GetInt32(reader.GetOrdinal("PlayerStatus"))

            };

        }

        return null;
    }

    public async Task<(List<PlayerView> Players, int TotalCount)> GetAllPlayersAsync(
        int? playerId,
        string? kfupmId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var players = new List<PlayerView>();
        int totalCount = 0;

        using var conn = new SqlConnection(_connection.ConnectionString);
        using var cmd = new SqlCommand("dbo.SP_SearchPlayers", conn)
        {
            CommandType = CommandType.StoredProcedure
        };

        cmd.Parameters.AddWithValue("@PlayerId", (object?)playerId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@KFUPMId", (object?)kfupmId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
        cmd.Parameters.AddWithValue("@PageSize", pageSize);

        var totalParam = new SqlParameter("@TotalCount", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        cmd.Parameters.Add(totalParam);

        await conn.OpenAsync();

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            players.Add(new PlayerView
            {
                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                PlayerType = reader.GetInt32(reader.GetOrdinal("PlayerType")),
                PlayerStatus = reader.GetInt32(reader.GetOrdinal("PlayerStatus")),
                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                KFUPMId = reader.GetString(reader.GetOrdinal("KFUPMId")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                SecondName = reader.IsDBNull(reader.GetOrdinal("SecondName")) ? null : reader.GetString(reader.GetOrdinal("SecondName")),
                ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                NationalityId = reader.GetInt32(reader.GetOrdinal("NationalityId"))
            });
        }
        reader.Close();
        totalCount = totalParam.Value != DBNull.Value ? (int)totalParam.Value : 0;

        return (players, totalCount);
    }



    public async Task<bool> AssignPlayerToTeamAsync(PlayerTeam playerTeam)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        await connection.OpenAsync();



        using var command = new SqlCommand("SP_AssignPlayerToTeam", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@PlayerId", playerTeam.PlayerId);
        command.Parameters.AddWithValue("@TeamId", playerTeam.TeamId);
        command.Parameters.AddWithValue("@JoinedAt", DateTime.Now);
        command.Parameters.AddWithValue("@PlayerPosition", playerTeam.PlayerPosition);
        command.Parameters.AddWithValue("@PlayerRole", playerTeam.PlayerRole);

        await command.ExecuteNonQueryAsync();
        return true;

    }

    public async Task<bool> IsPlayerAlreadyAssignedAsync(int playerId, int tournamentId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("CheckPlayerAlreadyAssigned", connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PlayerId", playerId);
        command.Parameters.AddWithValue("@TournamentId", tournamentId);

        await connection.OpenAsync();

        var result = await command.ExecuteScalarAsync();

        return result != null && Convert.ToBoolean(result);
    }


    public async Task<bool> IsUserAlreadyPlayerAsync(int userId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        await connection.OpenAsync();

        var query = @"
        SELECT 1
        FROM AspNetUsers U
        INNER JOIN People P ON U.PersonId = P.PersonId
        INNER JOIN Players PL ON PL.PersonId = P.PersonId
        WHERE U.Id = @UserId";

        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UserId", userId);

        var result = await command.ExecuteScalarAsync();
        return result != null;
    }


}
