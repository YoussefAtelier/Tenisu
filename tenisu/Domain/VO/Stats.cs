namespace tenisu.Domain.VO
{
    public class Stats
    {
        public int Wins { get; }
        public int Matches { get; }

        public Stats(int wins, int matches)
        {
            Wins = wins;
            Matches = matches;
        }
    }
}
