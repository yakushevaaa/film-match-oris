using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmMatch.Domain.Constants;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace FilmMatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FilmController : ControllerBase
    {
        private readonly IDbContext _dbContext;

        public FilmController(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var films = await _dbContext.Films
                .Include(f => f.Category)
                .ToListAsync();
            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var film = await _dbContext.Films
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (film == null)
                return NotFound();

            return Ok(film);
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Create([FromBody] Film film)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _dbContext.Films.AddAsync(film);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = film.Id }, film);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Film film)
        {
            if (id != film.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingFilm = await _dbContext.Films
                .Include(f => f.Category)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (existingFilm == null)
                return NotFound();

            _dbContext.Entry(existingFilm).CurrentValues.SetValues(film);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var film = await _dbContext.Films.FindAsync(id);
            if (film == null)
                return NotFound();

            _dbContext.Films.Remove(film);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("GetAllFilms")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllFilms()
        {
            var films = await _dbContext.Films
                .Include(f => f.Category)
                .Select(f => new {
                    f.Id,
                    f.Title,
                    f.ReleaseDate,
                    f.ImageUrl,
                    f.LongDescription,
                    f.ShortDescription,
                    Category = f.Category == null ? null : new {
                        f.Category.Id,
                        f.Category.Name
                    }
                })
                .ToListAsync();
            return Ok(films);
        }
    }
} 