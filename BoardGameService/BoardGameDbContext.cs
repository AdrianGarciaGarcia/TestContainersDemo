using Microsoft.EntityFrameworkCore;

namespace BoardGameService;

public sealed class BoardGameDbContext : DbContext
{
    public BoardGameDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BoardGame> BoardGames { get; set; }
}
