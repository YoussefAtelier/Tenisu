using System.Text.Json;

namespace tenisu.Infrastructure.DTO
{
    public class PlayerDto
    {
        public int Player_Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Shortname { get; set; }
        public string Sex { get; set; }
        public string Player_Picture { get; set; }
        public string Country_Code { get; set; }
        public string Country_Picture { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Matches { get; set; } // Raw JSON from DB
        public List<Match> MatchList => JsonSerializer.Deserialize<List<Match>>(Matches ?? "[]");
    }

    public class Match
    {
        public Guid Match_Id { get; set; }
        public Guid Player_Id { get; set; }
        public bool Result { get; set; }
    }
}
