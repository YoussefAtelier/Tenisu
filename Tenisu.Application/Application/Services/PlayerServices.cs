using tenisu.Application.Contracts;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;
using tenisu.Infrastructure.PlayerRepo;

namespace tenisu.Application.Services
{
    public class PlayerServices : IPlayersServices
    {
        private readonly IPlayerRepository _repository;
        private readonly IPlayerStatisticsService _statsService;

        public PlayerServices(IPlayerRepository repository, IPlayerStatisticsService statsService)
        {
            _repository = repository;
            _statsService = statsService;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            var result = await _repository.GetAllAsync();
            return result;
        }

        public async Task<Player?> GetPlayerById(int id)
        {
            return  await _repository.GetByIdAsync(id);

        }

        public async Task<StatisticsDto> GetStatistics()
        {
            var players = await _repository.GetAllAsync();
            return _statsService.CalculateStatistics(players);
        }
    }
}
