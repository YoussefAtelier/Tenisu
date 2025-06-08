using Microsoft.AspNetCore.Mvc;
using Moq;
using tenisu.Api.controllers;
using tenisu.Application.Contracts;
using tenisu.Domain.DTO;
using tenisu.Domain.Entities;
using tenisu.Domain.VO;

[TestFixture]
public class PlayersControllerTests
{
    private Mock<IPlayersServices> _mockService;
    private PlayersController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IPlayersServices>();
        _controller = new PlayersController(_mockService.Object);
    }

    [Test]
    public async Task GetPlayers_ReturnsListOfPlayers()
    {
        // Arrange
        var players = new List<Player>
        {
            new Player (1, "Roger","Federer", "RF", "M", new Country("CHE", ""),"", new PlayerData(1,2700, 80, 185, 40, new List<PlayerMatch>()) ),
            new Player (2, "Rafael","Nadal", "RF", "M", new Country("Esp", ""),"", new PlayerData(2,2700, 80, 185, 40, new List<PlayerMatch>()) )
        };

        _mockService.Setup(s => s.GetAllPlayers()).ReturnsAsync(players);

        // Act
        var result = await _controller.GetPlayers();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.Value, Is.EqualTo(players));
    }

    [Test]
    public async Task GetPlayer_ExistingId_ReturnsPlayer()
    {
        // Arrange
        var player = new Player(1, "Roger", "Federer", "RF", "M", new Country("CHE", ""), "", new PlayerData(1, 2700, 80, 185, 40, new List<PlayerMatch>()));
        _mockService.Setup(s => s.GetPlayerById(1)).ReturnsAsync(player);

        // Act
        var result = await _controller.GetPlayer(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.That(okResult.Value, Is.EqualTo(player));
    }

    [Test]
    public async Task GetPlayer_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(s => s.GetPlayerById(It.IsAny<int>())).ReturnsAsync((Player)null);

        // Act
        var result = await _controller.GetPlayer(999);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result.Result);
    }

    [Test]
    public async Task GetStatistics_ReturnsFormattedResult()
    {
        // Arrange
        var stats = new StatisticsDto
        {
            BestWinRatioCountry = "Spain",
            AverageBMI = 22.5,
            MedianHeight = 185.0
        };

        _mockService.Setup(s => s.GetStatistics()).ReturnsAsync(stats);

        // Act
        var result = await _controller.GetStatistics();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

    }
}
