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
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var categories = _context.Categories
                .Include(c => c.Games)
                .Select(c => new CategoriesDTORead
                {
                    Id = c.Id,
                    Name = c.Name,
                    Games = c.Games.Select(g => new GamesDTORead
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
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoriesDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var category = new Category
            {
                Name = categoryDto.Name
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] CategoriesDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = categoryDto.Name;
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
