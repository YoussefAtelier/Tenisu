using tenisu.Domain.DTO;
using tenisu.Domain.Entities;

namespace tenisu.Application.Contracts
{
    public interface IPlayersServices
    {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task<Player?> GetPlayerById(int id);
        Task<StatisticsDto> GetStatistics();
    }
}
