using BoardGameService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<BoardGameDbContext>(
        options =>
        {
            options.UseInMemoryDatabase("BoardGames");
        });

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet(
    "api/boardgames",
    async (BoardGameDbContext context) =>
    {
        return await context.BoardGames.ToListAsync();
    })
    .WithName("GetBoardGames")
    .WithOpenApi();

app.Run();