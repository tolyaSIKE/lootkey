using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LootkeyAPI.Data;
using LootkeyAPI.Models;

namespace LootkeyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return NotFound();
            return game;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Game>>> Search(
            [FromQuery] string? query,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? genre,
            [FromQuery] int? year
        )
        {
            var games = _context.Games.AsQueryable();

            if (!string.IsNullOrEmpty(query))
                games = games.Where(g => g.Title.Contains(query));

            if (minPrice.HasValue)
                games = games.Where(g => g.Price >= minPrice);

            if (maxPrice.HasValue)
                games = games.Where(g => g.Price <= maxPrice);

            if (!string.IsNullOrEmpty(genre))
                games = games.Where(g => g.Genre == genre);

            if (year.HasValue)
                games = games.Where(g => g.Year == year);

            return await games.ToListAsync();
        }
    }
}