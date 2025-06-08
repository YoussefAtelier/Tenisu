using Microsoft.Extensions.Logging;
using Moq;
using tenisu.Application.Contracts;
using tenisu.Application.Services;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;
using tenisu.Domain.VO;
using tenisu.Infrastructure.PlayerRepo;
using Tenisu.Application.Exceptions;
using Tenisu.Infrastructure.Infrastructure.Exceptions;

[TestFixture]
public class PlayerServicesTests
{
    private Mock<IPlayerRepository> _mockRepo;
    private Mock<IPlayerStatisticsService> _mockStatsService;
    private Mock<ILogger<PlayerServices>> _mockLogger;
    private PlayerServices _service;

    [SetUp]
    public void Setup()
    {
        _mockRepo = new Mock<IPlayerRepository>();
        _mockStatsService = new Mock<IPlayerStatisticsService>();
        _mockLogger = new Mock<ILogger<PlayerServices>>();

        _service = new PlayerServices(_mockRepo.Object, _mockStatsService.Object, _mockLogger.Object);
    }


    private List<Player> GetSamplePlayers() => new List<Player>
    {
        new Player(1, "Roger", "Federer", "RF", "M", new Country("CHE", ""), "", new PlayerData(1, 2700, 80, 185, 40, new List<PlayerMatch>())),
        new Player(2, "Rafael", "Nadal", "RN", "M", new Country("ESP", ""), "", new PlayerData(2, 2700, 80, 185, 40, new List<PlayerMatch>()))
    };


    [Test]
    public async Task GetAllPlayers_ReturnsAllPlayers()
    {
        // Arrange
        var players = GetSamplePlayers();

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

        // Act
        var result = await _service.GetAllPlayers();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, ((List<Player>)result).Count);
        CollectionAssert.AreEqual(players, result);
    }

    [Test]
    public void GetAllPlayers_WhenRepositoryThrows_ThrowsDataAccessException()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception("DB error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DataAccessException>(async () => await _service.GetAllPlayers());
        Assert.That(ex.Message, Does.Contain("error occurred while fetching players"));
    }

    [Test]
    public async Task GetPlayerById_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var player = GetSamplePlayers().First();
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
    public void GetPlayerById_WhenRepositoryThrows_ThrowsDataAccessException()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("DB error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<DataAccessException>(async () => await _service.GetPlayerById(1));
        Assert.That(ex.Message, Does.Contain("error occurred while fetching player with ID"));
    }

    [Test]
    public async Task GetStatistics_CallsRepositoryAndStatsService()
    {
        // Arrange
        var players = GetSamplePlayers();

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

    [Test]
    public void GetStatistics_WhenStatsServiceThrows_ThrowsStatisticsCalculationException()
    {
        // Arrange
        var players = GetSamplePlayers();
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);
        _mockStatsService.Setup(s => s.CalculateStatistics(players)).Throws(new Exception("Calculation error"));

        // Act & Assert
        var ex = Assert.ThrowsAsync<StatisticsCalculationException>(async () => await _service.GetStatistics());
        Assert.That(ex.Message, Does.Contain("Unable to calculate statistics"));
    }
}
