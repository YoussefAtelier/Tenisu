using tenisu.Application.Contracts;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;

namespace tenisu.Application.Services
{
    public class PlayerStatisticsService : IPlayerStatisticsService
    {
        public StatisticsDto CalculateStatistics(IEnumerable<Player> players)
        {
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

            var avgBmi = players.Average(p => p.CalculateBMI());

            var heights = players.Select(p => p.Data.Height).OrderBy(h => h).ToList();
            double medianHeight = heights.Count % 2 == 0
                ? (heights[heights.Count / 2 - 1] + heights[heights.Count / 2]) / 2.0
                : heights[heights.Count / 2];

            return new StatisticsDto
            {
                BestWinRatioCountry = countryWinRatios?.Country,
                AverageBMI = Math.Round(avgBmi, 2),
                MedianHeight = medianHeight
            };
        }
    }
}
