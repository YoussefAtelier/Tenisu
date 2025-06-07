using tenisu.Domain.VO;

namespace tenisu.Domain.Entities
{
    public class PlayerData
    {
        public int Rank { get; }
        public int Points { get; }
        public int Weight { get; }
        public int Height { get; }
        public int Age { get; }
        public List<PlayerMatch> Matches { get; }

        public Stats Stats => new Stats(Matches.Count(m => m.HasWon), Matches.Count);

        public PlayerData(int rank, int points, int weight, int height, int age, List<PlayerMatch> matches)
        {
            Rank = rank;
            Points = points;
            Weight = weight;
            Height = height;
            Age = age;
            Matches = matches;
        }
    }
}
