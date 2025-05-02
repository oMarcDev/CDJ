using BackEND.Data;
using BackEND.DTO;
using BackEND.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GamesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllGames()
        {
            var games = _context.Games
                .Include(g => g.Genre)
                .Include(g => g.Category)
                .Select(g => new GamesDTORead
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate,
                    Price = g.Price,
                    GenreId = g.Genre != null ? g.Genre.Id : null,
                    CategoryId = g.Category != null ? g.Category.Id : null
                })
                .ToList();
            return Ok(games);
        }
        [HttpGet("{id}")]
        public IActionResult GetGameById(int id)
        {
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
        [HttpPost]
        public IActionResult CreateGame([FromBody] GamesDTO gameDto)
        {
            if (gameDto == null)
            {
                return BadRequest();
            }

            var genre = _context.Genres.Find(gameDto.GenreId);
            var category = _context.Categories.Find(gameDto.CategoryId);
            var game = new Game
            {
                Name = gameDto.Name,
                ReleaseDate = gameDto.ReleaseDate,
                Price = gameDto.Price,
                Genre = genre,
                Category = category
            };
            _context.Games.Add(game);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, [FromBody] GamesDTO gameDto)
        {
            if (gameDto == null)
            {
                return BadRequest();
            }
            var genre = _context.Genres.Find(gameDto.GenreId);
            var category = _context.Categories.Find(gameDto.CategoryId);
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            game.Name = gameDto.Name;
            game.ReleaseDate = gameDto.ReleaseDate;
            game.Price = gameDto.Price;
            game.Genre = genre;
            game.Category = category;
            _context.Games.Update(game);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }
            _context.Games.Remove(game);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
