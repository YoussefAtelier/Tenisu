namespace tenisu.Domain.Entities
{
    public class PlayerMatch
    {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public bool HasWon { get; set; }
    }
}
