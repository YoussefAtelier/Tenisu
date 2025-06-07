using tenisu.Domain.Entities;

namespace tenisu.Domain.Factory
{
    public interface IPlayerFactory
    {
        IPlayerAR Hydrate(Player player);
    }
}
