using tenisu.Domain.DTO;
using tenisu.Domain.Entities;

namespace tenisu.Application.Contracts
{
    public interface IPlayerStatisticsService
    {
        StatisticsDto CalculateStatistics(IEnumerable<Player> players);

    }
}
