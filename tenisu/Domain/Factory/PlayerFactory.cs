using tenisu.Domain.Entities;

namespace tenisu.Domain.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayerAR Hydrate(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            return new PlayerAR(player);
        }
    }
}
