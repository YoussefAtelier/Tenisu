using Microsoft.AspNetCore.Mvc;
using tenisu.Application.Contracts;
using tenisu.Domain.Entities;

namespace tenisu.Api.controllers;

[ApiController]
[Route("[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayersServices _service;

    public PlayersController(IPlayersServices service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Player>>> GetPlayers()
    {
        var playersList = await _service.GetAllPlayers();
        return Ok(playersList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _service.GetPlayerById(id);
        if (player == null) return NotFound();
        return Ok(player);
    }

    [HttpGet("/statistics")]
    public async Task<ActionResult<object>> GetStatistics()
    {
        var result = await _service.GetStatistics();
        return Ok(new
        {
            BestWinRatioCountry = result.BestWinRatioCountry,
            AverageBMI = result.AverageBMI,
            MedianHeight = result.MedianHeight
        });
    }
}
