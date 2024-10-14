using Microsoft.EntityFrameworkCore;

namespace BoardGameService;

public class BoardGameRepository(BoardGameDbContext context)
{
    public async Task<IEnumerable<BoardGame>> GetBoardGamesAsync() { return await context.BoardGames.ToListAsync(); }
    public async Task<BoardGame> GetBoardGameAsync(Guid id) { return await context.BoardGames.FindAsync(id); }
    public async Task AddBoardGameAsync(BoardGame boardGame) { await context.BoardGames.AddAsync(boardGame); }
}
