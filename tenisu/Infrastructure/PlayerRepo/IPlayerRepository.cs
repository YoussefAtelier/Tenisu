using tenisu.Domain.Entities;

namespace tenisu.Infrastructure.PlayerRepo
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player?> GetByIdAsync(int id);
    }
}
