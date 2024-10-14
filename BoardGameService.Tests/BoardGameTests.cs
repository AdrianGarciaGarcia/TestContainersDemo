using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace BoardGameService.Tests;

public class BoardGameTests
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();
    private BoardGameRepository _repository;
    private Guid _boardGameId;

    [OneTimeSetUp]
    public async Task Setup()
    {
        await _dbContainer.StartAsync();
        var context = new BoardGameDbContext(
            new DbContextOptionsBuilder<BoardGameDbContext>()
                .UseSqlServer(_dbContainer.GetConnectionString())
                .Options);

        await context.Database.EnsureCreatedAsync();
        _repository = new BoardGameRepository(context);
        _boardGameId = Guid.NewGuid();
        await _repository.AddBoardGameAsync(new BoardGame(_boardGameId, "Catan"));
        await _repository.AddBoardGameAsync(new BoardGame(Guid.NewGuid(), "Terraforming Mars"));
        await context.SaveChangesAsync();
    }

    [Test]
    public void GetByIdShouldReturnCatan()
    {
        var boardGame = _repository.GetBoardGameAsync(_boardGameId).Result;
        Assert.That(boardGame.Name, Is.EqualTo("Catan"));
    }

    [Test]
    public void ShouldReturnTwoBoardGames()
    {
        var boardGame = _repository.GetBoardGameAsync(_boardGameId).Result;
        var boardGames = _repository.GetBoardGamesAsync().Result;
        Assert.That(_repository.GetBoardGamesAsync().Result.Count(), Is.EqualTo(2));
    }


    [OneTimeTearDown]
    public async Task OneTimeTearDown() { await _dbContainer.DisposeAsync(); }
}