using Dapper;
using Moq;
using Moq.Dapper;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tenisu.Domain.Entities;
using tenisu.Infrastructure;
using tenisu.Infrastructure.DTO;
using tenisu.Infrastructure.PlayerRepo;

[TestFixture]
public class PlayerRepositoryTests
{
    private Mock<IDbConnection> _mockConnection;
    private Mock<IDbConnectionFactory> _mockFactory;
    private PlayerRepository _repository;

    [SetUp]
    public void Setup()
    {
        _mockConnection = new Mock<IDbConnection>();
        _mockFactory = new Mock<IDbConnectionFactory>();
        _mockFactory.Setup(f => f.CreateConnection()).Returns(_mockConnection.Object);
        _repository = new PlayerRepository(_mockFactory.Object);
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
