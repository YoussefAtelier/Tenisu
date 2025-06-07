using System.Text.Json;
using tenisu.Domain.Entities;
using tenisu.Domain.VO;
using tenisu.Infrastructure.DTO;

namespace tenisu.Infrastructure
{
    public static class PlayerMapper
    {
        public static Player MapToDomain(this PlayerDto dbPlayer)
        {
            // Parse match list from raw JSON
            var matches = JsonSerializer.Deserialize<List<PlayerMatch>>(dbPlayer.Matches ?? "[]");

            var playerData = new PlayerData(
                rank: dbPlayer.Rank,
                points: dbPlayer.Points,
                weight: dbPlayer.Weight,
                height: dbPlayer.Height,
                age: dbPlayer.Age,
                matches : matches
            );

            var country = new Country(
                code: dbPlayer.Country_Code,
                picture: dbPlayer.Country_Picture
            );

            return new Player(
                id: dbPlayer.Player_Id, // Or if `int` ID exists, use that
                firstName: dbPlayer.Firstname,
                lastName: dbPlayer.Lastname,
                shortName: dbPlayer.Shortname,
                sex: dbPlayer.Sex,
                country: country,
                picture: dbPlayer.Player_Picture,
                data: playerData
            );
        }
    }

}
