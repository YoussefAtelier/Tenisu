using tenisu.Application.Contracts;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;

namespace tenisu.Application.Services
{
    public class PlayerStatisticsService : IPlayerStatisticsService
    {
        public StatisticsDto CalculateStatistics(IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException(nameof(players), "Player collection cannot be null.");
            }
            var countryWinRatios = players
                .Where(p => p.Data.Stats.Matches > 0)
                .GroupBy(p => p.Country.Code)
                .Select(g => new
                {
                    Country = g.Key,
                    Ratio = g.Sum(p => p.Data.Stats.Wins) / (double)g.Sum(p => p.Data.Stats.Matches)
                })
                .OrderByDescending(x => x.Ratio)
                .FirstOrDefault();

            var validPlayersForBmi = players.Where(p => p.Data != null && p.Data.Height > 0 && p.Data.Weight > 0);

            var avgBmi = validPlayersForBmi.Any()? validPlayersForBmi.Average(p => p.Data.Height == 0 ? 0 : Math.Round((p.Data.Weight / 1000) / Math.Pow(p.Data.Height / 100.0, 2), 2)) : 0;

            var heights = players.Select(p => p.Data.Height).OrderBy(h => h).ToList();
            double medianHeight = 0;
            if (heights.Any())
            {
                medianHeight = heights.Count % 2 == 0
                    ? (heights[heights.Count / 2 - 1] + heights[heights.Count / 2]) / 2.0
                    : heights[heights.Count / 2];
            }

            return new StatisticsDto
            {
                BestWinRatioCountry = countryWinRatios?.Country,
                AverageBMI = Math.Round(avgBmi, 2),
                MedianHeight = medianHeight
            };
        }
    }
}
