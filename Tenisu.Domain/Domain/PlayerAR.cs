using tenisu.Domain.Entities;
using tenisu.Domain.VO;

namespace tenisu.Domain
{
    public class PlayerAR : IPlayerAR
    {
        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _shortName;
        private readonly string _sex;
        private readonly Country _country;
        private readonly string _picture;
        private readonly PlayerData _data;

        public PlayerAR(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            _id = player.Id;
            _firstName = player.FirstName;
            _lastName = player.LastName;
            _shortName = player.ShortName;
            _sex = player.Sex;
            _country = player.Country;
            _picture = player.Picture;
            _data = player.Data;
        }
    }
}
