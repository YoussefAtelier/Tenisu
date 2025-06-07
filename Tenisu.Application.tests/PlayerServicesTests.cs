using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using tenisu.Application.Contracts;
using tenisu.Application.Services;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;
using tenisu.Domain.VO;
using tenisu.Infrastructure.PlayerRepo;

[TestFixture]
public class PlayerServicesTests
{
    private Mock<IPlayerRepository> _mockRepo;
    private Mock<IPlayerStatisticsService> _mockStatsService;
    private PlayerServices _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IPlayerRepository>();
        _mockStatsService = new Mock<IPlayerStatisticsService>();
        _service = new PlayerServices(_mockRepo.Object, _mockStatsService.Object);
    }

    [Test]
    public async Task GetAllPlayers_ReturnsAllPlayers()
    {
        // Arrange
        var players = new List<Player>
        {
            new Player (1, "Roger","Federer", "RF", "M", new Country("CHE", ""),"", new PlayerData(1, 2700, 80, 185, 40, new List<PlayerMatch>()) ),
            new Player (2, "Rafael","Nadal", "RN", "M", new Country("Esp", ""),"", new PlayerData(2, 2700, 80, 185, 40, new List<PlayerMatch>()) )
        };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

        // Act
        var result = await _service.GetAllPlayers();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, ((List<Player>)result).Count);
        CollectionAssert.AreEqual(players, result);
    }

    [Test]
    public async Task GetPlayerById_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var player = new Player(2, "Rafael", "Nadal", "RN", "M", new Country("Esp", ""), "", new PlayerData(2, 2700, 80, 185, 40, new List<PlayerMatch>()));
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);

        // Act
        var result = await _service.GetPlayerById(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(player.Id, result.Id);
    }

    [Test]
    public async Task GetPlayerById_NonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Player)null);

        // Act
        var result = await _service.GetPlayerById(999);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetStatistics_CallsRepositoryAndStatsService()
    {
        // Arrange
        var players = new List<Player>
        {
            new Player (1, "Roger","Federer", "RF", "M", new Country("CHE", ""),"", new PlayerData(1, 2700, 80, 185, 40, new List<PlayerMatch>(){new PlayerMatch {PlayerId =1,MatchId = 1, HasWon = true }, new PlayerMatch { PlayerId = 1, MatchId = 2, HasWon = true }, new PlayerMatch { PlayerId = 1, MatchId = 2, HasWon = true } }  ) ),
            new Player (2, "Rafael","Nadal", "RN", "M", new Country("Esp", ""),"", new PlayerData(2, 2700, 80, 185, 40, new List<PlayerMatch>(){new PlayerMatch {PlayerId =2,MatchId = 4, HasWon = true },new PlayerMatch {PlayerId =2,MatchId = 1, HasWon = false },new PlayerMatch {PlayerId =2,MatchId = 6, HasWon = true }}) )};

        var expectedStats = new StatisticsDto
        {
            BestWinRatioCountry = "CHE",
            AverageBMI = 23.5,
            MedianHeight = 185
        };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);
        _mockStatsService.Setup(s => s.CalculateStatistics(players)).Returns(expectedStats);

        // Act
        var result = await _service.GetStatistics();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedStats.BestWinRatioCountry, result.BestWinRatioCountry);
        Assert.AreEqual(expectedStats.AverageBMI, result.AverageBMI);
        Assert.AreEqual(expectedStats.MedianHeight, result.MedianHeight);
    }
}
