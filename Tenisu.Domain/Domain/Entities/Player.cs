using tenisu.Domain.VO;

namespace tenisu.Domain.Entities
{
    public class Player
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string ShortName { get; }
        public string Sex { get; }
        public Country Country { get; }
        public string Picture { get; }
        public PlayerData Data { get; }

        public Player(int id, string firstName, string lastName, string shortName, string sex, Country country, string picture, PlayerData data)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ShortName = shortName;
            Sex = sex;
            Country = country;
            Picture = picture;
            Data = data;
        }

        public double WinRatio() => Data.Stats.Matches == 0 ? 0 : (double)Data.Stats.Wins / Data.Stats.Matches;

        public double CalculateBMI() => Data.Height == 0 ? 0 : Math.Round((Data.Weight/1000) / Math.Pow(Data.Height / 100.0, 2), 2);
    }
}
