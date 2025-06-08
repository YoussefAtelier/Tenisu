using Dapper;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Dapper;
using System.Data;
using tenisu.Infrastructure;
using tenisu.Infrastructure.DTO;
using tenisu.Infrastructure.PlayerRepo;
using Tenisu.Infrastructure.Infrastructure.Exceptions;

[TestFixture]
public class PlayerRepositoryTests
{
    private Mock<IDbConnection> _mockConnection;
    private Mock<IDbConnectionFactory> _mockFactory;
    private Mock<ILogger<PlayerRepository>> _mockLogger;
    private PlayerRepository _repository;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<IDbConnection>();
        _mockFactory = new Mock<IDbConnectionFactory>();
        _mockFactory.Setup(f => f.CreateConnection()).Returns(_mockConnection.Object);
        _mockLogger = new Mock<ILogger<PlayerRepository>>();
        _repository = new PlayerRepository(_mockFactory.Object, _mockLogger.Object);

    }

    [Test]
    public async Task GetAllAsync_ReturnsMappedPlayers()
    {
        // Arrange
        var dtoList = new List<PlayerDto>
        {
            new PlayerDto { Player_Id = 1, Firstname = "P1", Lastname = "LP1" }
        };

        _mockConnection.SetupDapperAsync(c => c.QueryAsync<PlayerDto>(It.IsAny<string>(), null, null, null, null))
                       .ReturnsAsync(dtoList);

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("P1", result.First().FirstName);
    }

    [Test]
    public async Task GetByIdAsync_ExistingId_ReturnsMappedPlayer()
    {
        // Arrange
        var dto = new PlayerDto { Player_Id = 1, Firstname = "P1", Lastname = "LP1" };

        _mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<PlayerDto>(
            It.IsAny<string>(),
            It.IsAny<object>(),
            null, null, null))
            .ReturnsAsync(dto);

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("P1", result.FirstName);
    }

    [Test]
    public async Task GetByIdAsync_InvalidId_ReturnsNull()
    {
        // Arrange
        _mockConnection.SetupDapperAsync(c => c.QuerySingleOrDefaultAsync<PlayerDto>(
            It.IsAny<string>(),
            It.IsAny<object>(),
            null, null, null))
            .ReturnsAsync((PlayerDto)null);

        // Act
        var result = await _repository.GetByIdAsync(-1);

        // Assert
        Assert.IsNull(result);
    }
}
