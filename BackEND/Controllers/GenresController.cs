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
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var genres = _context.Genres
                .Include(g => g.Games)
                .Select(g => new GenresDTORead
                {
                    Id = g.Id,
                    Name = g.Name,
                    Games = g.Games.Select(g => new GamesDTORead
                    {
                        Id = g.Id,
                        Name = g.Name,
                        ReleaseDate = g.ReleaseDate,
                        Price = g.Price,
                        GenreId = g.Genre != null ? g.Genre.Id : null,
                        CategoryId = g.Category != null ? g.Category.Id : null
                    }).ToList()
                })
                .ToList();
            return Ok(genres);
        }
        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }
        [HttpPost]
        public IActionResult CreateGenre([FromBody] GenresDTO genreDto)
        {
            if (genreDto == null)
            {
                return BadRequest();
            }
            var genre = new Genre
            {
                Name = genreDto.Name
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] GenresDTO genreDto)
        {
            if (genreDto == null)
            {
                return BadRequest();
            }
            var genre = _context.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = genreDto.Name;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
