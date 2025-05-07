using Microsoft.Data.SqlClient;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Enums;
using SoccerKFUPM.Domain.Enums;
using SoccerKFUPM.Domain.IRepository;
using System.Data;

namespace SoccerKFUPM.Infrastructure.Repository;

public class RequestRepository : IRequestRepository
{
    private readonly IDbConnection _connection;

    public RequestRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> CreateRequestAsync(Request request)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_CreateRequest", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@PlayerId", request.PlayerId);
        command.Parameters.AddWithValue("@TeamId", request.TeamId);
        command.Parameters.AddWithValue("@RequestType", (int)request.RequestType);
        command.Parameters.AddWithValue("@Status", (int)request.Status);
        command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);
        command.Parameters.AddWithValue("@PreferredPosition", (int)request.PreferredPosition);

        var outputId = new SqlParameter("@RequestId", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputId);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        var id = (int?)outputId.Value;
        return id > 0;
    }

    public async Task<Request?> GetRequestByIdAsync(int requestId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_GetRequestById", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@RequestId", requestId);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return new Request
            {
                RequestId = requestId,
                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                RequestType = (RequestType)reader.GetInt32(reader.GetOrdinal("RequestType")),
                Status = (RequestStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                ProcessedAt = reader.IsDBNull(reader.GetOrdinal("ProcessedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("ProcessedAt")),
                ProcessedByUserId = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserId")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserId")),
                PreferredPosition = (PlayerPosition)reader.GetInt32(reader.GetOrdinal("PreferredPosition"))
            };
        }

        return null;
    }

    public async Task<bool> HasPendingTeamRequestAsync(int playerId, int teamId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_HasPendingTeamRequest", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@PlayerId", playerId);
        command.Parameters.AddWithValue("@TeamId", teamId);

        var outputParam = new SqlParameter("@HasPending", SqlDbType.Bit)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputParam);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        return (bool)outputParam.Value;
    }

    public async Task<bool> IsPlayerInTeamAsync(int playerId, int teamId)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_IsPlayerInTeam", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@PlayerId", playerId);
        command.Parameters.AddWithValue("@TeamId", teamId);

        var outputParam = new SqlParameter("@IsInTeam", SqlDbType.Bit)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(outputParam);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();

        return (bool)outputParam.Value;
    }

    public async Task<(List<Request> Requests, int TotalCount)> GetRequestsByPlayerAsync(
        int playerId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_GetRequestsByPlayer", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@PlayerId", playerId);
        command.Parameters.AddWithValue("@PageNumber", pageNumber);
        command.Parameters.AddWithValue("@PageSize", pageSize);

        var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(totalCountParam);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        var requests = new List<Request>();
        while (await reader.ReadAsync())
        {
            requests.Add(new Request
            {
                RequestId = reader.GetInt32(reader.GetOrdinal("RequestId")),
                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                RequestType = (RequestType)reader.GetInt32(reader.GetOrdinal("RequestType")),
                Status = (RequestStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                ProcessedAt = reader.IsDBNull(reader.GetOrdinal("ProcessedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("ProcessedAt")),
                ProcessedByUserId = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserId")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserId")),
                PreferredPosition = (PlayerPosition)reader.GetInt32(reader.GetOrdinal("PreferredPosition"))
            });
        }

        return (requests, (int)totalCountParam.Value);
    }

    public async Task<(List<Request> Requests, int TotalCount)> GetRequestsByTeamAsync(
        int teamId,
        int pageNumber = 1,
        int pageSize = 10)
    {
        using var connection = new SqlConnection(_connection.ConnectionString);
        using var command = new SqlCommand("SP_GetRequestsByTeam", connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        command.Parameters.AddWithValue("@TeamId", teamId);
        command.Parameters.AddWithValue("@PageNumber", pageNumber);
        command.Parameters.AddWithValue("@PageSize", pageSize);

        var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };
        command.Parameters.Add(totalCountParam);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        var requests = new List<Request>();
        while (await reader.ReadAsync())
        {
            requests.Add(new Request
            {
                RequestId = reader.GetInt32(reader.GetOrdinal("RequestId")),
                PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                RequestType = (RequestType)reader.GetInt32(reader.GetOrdinal("RequestType")),
                Status = (RequestStatus)reader.GetInt32(reader.GetOrdinal("Status")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                ProcessedAt = reader.IsDBNull(reader.GetOrdinal("ProcessedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("ProcessedAt")),
                ProcessedByUserId = reader.IsDBNull(reader.GetOrdinal("ProcessedByUserId")) ? null : reader.GetString(reader.GetOrdinal("ProcessedByUserId")),
                PreferredPosition = (PlayerPosition)reader.GetInt32(reader.GetOrdinal("PreferredPosition"))
            });
        }

        return (requests, (int)totalCountParam.Value);
    }
}