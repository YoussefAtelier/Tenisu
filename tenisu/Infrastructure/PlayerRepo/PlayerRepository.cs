using Dapper;
using tenisu.Domain.Entities;
using tenisu.Infrastructure.DTO;

namespace tenisu.Infrastructure.PlayerRepo
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public PlayerRepository(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            using var db = _dbFactory.CreateConnection();
            var sql = "SELECT * FROM get_grouped_player_data();";

            var queryResult = await db.QueryAsync<PlayerDto>(sql);

            return queryResult.Select(p => p.MapToDomain()).ToList();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            using var db = _dbFactory.CreateConnection();
            var sql = "SELECT * FROM get_player_data(@SelectedPlayer);";
            var param = new DynamicParameters();
            param.Add("SelectedPlayer", id);
            var queryResult = await db.QuerySingleOrDefaultAsync<PlayerDto>(sql, param);

            if (queryResult == null)
                return null; // or throw if you want

            return queryResult.MapToDomain();
        }
    }
}