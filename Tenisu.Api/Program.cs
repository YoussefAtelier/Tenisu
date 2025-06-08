using System;
using tenisu.Application.Contracts;
using tenisu.Application.Services;
using tenisu.Domain.Factory;
using tenisu.Infrastructure;
using tenisu.Infrastructure.PlayerRepo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// DATABASE
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

// REPO
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();

// Services
builder.Services
    .AddSingleton<IPlayerFactory, PlayerFactory>()
    .AddSingleton<IPlayersServices, PlayerServices>()
    .AddSingleton<IPlayerFactory, PlayerFactory>()
    .AddSingleton<IPlayerStatisticsService, PlayerStatisticsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
