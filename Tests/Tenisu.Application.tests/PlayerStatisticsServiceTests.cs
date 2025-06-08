using tenisu.Application.Contracts;
using tenisu.Application.Services;
using tenisu.Domain.Entities;
using tenisu.Domain.VO;

[TestFixture]
public class PlayerStatisticsServiceTests
{
    private PlayerStatisticsService _service;

    [SetUp]
    public void Setup()
    {
        _service = new PlayerStatisticsService();
    }

    private List<Player> GetSamplePlayers() => new List<Player>
        {
            new Player (1, "Roger","Federer", "RF", "M", new Country("CHE", ""),"", new PlayerData(1, 2700, 80000, 185, 40, new List<PlayerMatch>(){new PlayerMatch {PlayerId =1,MatchId = 1, HasWon = true }, new PlayerMatch { PlayerId = 1, MatchId = 2, HasWon = true }, new PlayerMatch { PlayerId = 1, MatchId = 2, HasWon = true } }  ) ),
            new Player (2, "Rafael","Nadal", "RN", "M", new Country("Esp", ""),"", new PlayerData(2, 2700, 75000, 180, 40, new List<PlayerMatch>(){new PlayerMatch {PlayerId =2,MatchId = 4, HasWon = true },new PlayerMatch {PlayerId =2,MatchId = 1, HasWon = false },new PlayerMatch {PlayerId =2,MatchId = 6, HasWon = true }}))
        };


    [Test]
    public void CalculateStatistics_ReturnsCorrectBestWinRatioCountry()
    {
        // Arrange
        var players = GetSamplePlayers();


        // Act
        var result = _service.CalculateStatistics(players);

        // Assert
        Assert.AreEqual("CHE", result.BestWinRatioCountry);
    }

    [Test]
    public void CalculateStatistics_ReturnsCorrectAverageBMI()
    {
        // Arrange
        var players = GetSamplePlayers();


        double bmi1 = 80 / Math.Pow(1.85, 2);
        double bmi2 = 75 / Math.Pow(1.80, 2);
        double expectedAverage = Math.Round((bmi1 + bmi2) / 2, 2);

        // Act
        var result = _service.CalculateStatistics(players);

        // Assert
        Assert.AreEqual(expectedAverage, result.AverageBMI);
    }

    [Test]
    public void CalculateStatistics_ReturnsCorrectMedianHeight()
    {
        // Arrange
        var players = GetSamplePlayers();

        // Act
        var result = _service.CalculateStatistics(players);

        // Assert
        Assert.AreEqual(182.5, result.MedianHeight);
    }

    [Test]
    public void CalculateStatistics_NullPlayers_ThrowsArgumentNullException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => _service.CalculateStatistics(null));
    }

}
