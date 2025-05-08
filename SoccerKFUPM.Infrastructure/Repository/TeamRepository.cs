using Microsoft.Data.SqlClient;
using SoccerKFUPM.Domain.Entities;
using SoccerKFUPM.Domain.Entities.Views;
using SoccerKFUPM.Domain.IRepository;
using System.Data;

namespace SoccerKFUPM.Infrastructure.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IDbConnection _connection;

        public TeamRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AddTeamAsync(Team team)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_InsertTeam", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", team.Name);
            command.Parameters.AddWithValue("@Address", team.Address);
            command.Parameters.AddWithValue("@Website", team.Website);
            command.Parameters.AddWithValue("@NumberOfPlayers", team.NumberOfPlayers);
            command.Parameters.AddWithValue("@ManagerId", team.ManagerId);

            var outputId = new SqlParameter("@TeamId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(outputId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            var id = (int?)outputId.Value;
            return id > 0;
        }

        public async Task<bool> DeleteTeamAsync(int teamId)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_DeleteTeam", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TeamId", teamId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return true;
        }

        public async Task<(List<TeamView> teams, int totalCount)> SearchTeamsAsync(
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
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_SearchTeams", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", (object?)name ?? DBNull.Value);
            command.Parameters.AddWithValue("@PageNumber", pageNumber);
            command.Parameters.AddWithValue("@PageSize", pageSize);

            var totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            command.Parameters.Add(totalCountParam);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            var teams = new List<TeamView>();
            while (await reader.ReadAsync())
            {
                teams.Add(new TeamView
                {
                    TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    NumberOfPlayers = reader.GetInt32(reader.GetOrdinal("NumberOfPlayers")),
                    ManagerId = reader.GetInt32(reader.GetOrdinal("ManagerId")),
                    ManagerFirstName = reader.GetString(reader.GetOrdinal("ManagerFirstName")),
                    ManagerLastName = reader.GetString(reader.GetOrdinal("ManagerLastName"))

                });
            }

            var totalCount = (int)(totalCountParam.Value ?? 0);
            return (teams, totalCount);
        }

        public async Task<TeamView?> GetTeamByIdAsync(int teamId)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_GetTeamById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TeamId", teamId);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new TeamView
                {
                    TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    Website = reader.GetString(reader.GetOrdinal("Website")),
                    NumberOfPlayers = reader.GetInt32(reader.GetOrdinal("NumberOfPlayers")),
                    ManagerId = reader.GetInt32(reader.GetOrdinal("ManagerId")),
                    ManagerFirstName = reader.GetString(reader.GetOrdinal("ManagerFirstName")),
                    ManagerLastName = reader.GetString(reader.GetOrdinal("ManagerLastName"))
                };
            }

            return null;
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_UpdateTeam", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TeamId", team.TeamId);
            command.Parameters.AddWithValue("@Name", team.Name);
            command.Parameters.AddWithValue("@Address", team.Address);
            command.Parameters.AddWithValue("@Website", team.Website);
            command.Parameters.AddWithValue("@NumberOfPlayers", team.NumberOfPlayers);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            return true;
        }


        public async Task<bool> TeamExistsAsync(int teamId)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_CheckTeamExists", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TeamId", teamId);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result) == 1;
        }

        public async Task<bool> IsTeamInTournamentAsync(int teamId, int tournamentId)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("CheckTeamInTournament", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TeamId", teamId);
            command.Parameters.AddWithValue("@TournamentId", tournamentId);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result) == 1;
        }

        public async Task<bool> IsTeamInTournamentAsync(int TournamentTeamId)
        {
            using var connection = new SqlConnection(_connection.ConnectionString);
            using var command = new SqlCommand("SP_CheckTeamInTournamentByTournamentTeamId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@TournamentTeamId", TournamentTeamId);

            await connection.OpenAsync();
            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result) == 1;
        }


    }
}
