using Dapper;
using Microsoft.Extensions.Logging;
using tenisu.Domain.Entities;
using tenisu.Infrastructure.DTO;
using Tenisu.Infrastructure.Infrastructure.Exceptions;

namespace tenisu.Infrastructure.PlayerRepo
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnectionFactory _dbFactory;
        private readonly ILogger<PlayerRepository> _logger;


        public PlayerRepository(IDbConnectionFactory dbFactory, ILogger<PlayerRepository> logger)
        {
            _dbFactory = dbFactory;
            _logger = logger;

        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            try
            {
                using var db = _dbFactory.CreateConnection();
                var sql = "SELECT * FROM get_grouped_player_data();";

                var queryResult = await db.QueryAsync<PlayerDto>(sql);
                return queryResult.Select(p => p.MapToDomain()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all players.");
                throw new DataAccessException("Failed to retrieve players from the database.", ex);
            }
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            try
            {
                using var db = _dbFactory.CreateConnection();
                var sql = "SELECT * FROM get_player_data(@SelectedPlayer);";
                var param = new DynamicParameters();
                param.Add("SelectedPlayer", id);

                var queryResult = await db.QuerySingleOrDefaultAsync<PlayerDto>(sql, param);

                return queryResult?.MapToDomain();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving player with ID {PlayerId}.", id);
                throw new DataAccessException($"Failed to retrieve player with ID {id}.", ex);
            }
        }
    }
}