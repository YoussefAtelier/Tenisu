using Microsoft.Extensions.Logging;
using tenisu.Application.Contracts;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;
using tenisu.Infrastructure.PlayerRepo;
using Tenisu.Application.Exceptions;
using Tenisu.Infrastructure.Infrastructure.Exceptions;

namespace tenisu.Application.Services
{
    public class PlayerServices : IPlayersServices
    {
        private readonly IPlayerRepository _repository;
        private readonly IPlayerStatisticsService _statsService;
        private readonly ILogger<PlayerServices> _logger;


        public PlayerServices(IPlayerRepository repository, IPlayerStatisticsService statsService, ILogger<PlayerServices> logger)
        {
            _repository = repository;
            _statsService = statsService;
            _logger = logger;

        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
        {
            try
            {
                var result =  await _repository.GetAllAsync();
                return result.OrderByDescending(x => x.WinRatio());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all players.");
                throw new DataAccessException("An error occurred while fetching players.", ex);
            }
        }

        public async Task<Player?> GetPlayerById(int id)
        {

            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving player by ID {PlayerId}", id);
                throw new DataAccessException($"An error occurred while fetching player with ID {id}.", ex);
            }

        }

        public async Task<StatisticsDto> GetStatistics()
        {
            try
            {
                var players = await _repository.GetAllAsync();
                return _statsService.CalculateStatistics(players);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating player statistics.");
                throw new StatisticsCalculationException("Unable to calculate statistics.", ex);
            }
        }
    }
}
