using FilmMatch.Application.Features.Films.BookmarkFilm;
using FilmMatch.Application.Features.Films.DislikeFilm;
using FilmMatch.Application.Features.Films.GetRecommendations;
using FilmMatch.Application.Features.Films.LikeFilm;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmMatch.Domain.Constants;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Interfaces;
using MediatR;

namespace FilmMatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FilmController : ControllerBase
    {
        private readonly IDbContext _dbContext;
        private readonly IMediator _mediator;

        public FilmController(IDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
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
                .Where(f => f.Id == id)
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
                .FirstOrDefaultAsync();

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
        [Authorize]
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

        [HttpPost("Like/{filmId}")]
        public async Task<IActionResult> ToggleLike(Guid filmId)
        {
            return Ok(await _mediator.Send(new ToggleLikeFilmCommand(filmId)));
        }

        [HttpGet("AllLikedFilms")]
        public async Task<IActionResult> GetAllLikedFilms()
        {
            return Ok(await _mediator.Send(new GetLikedFilmsQuery()));
        }

        [HttpPost("Dislike/{filmId}")]
        public async Task<IActionResult> ToggleDislike(Guid filmId)
        {
            return Ok(await _mediator.Send(new ToggleDislikeFilmCommand(filmId)));
        }

        [HttpGet("AllDislikedFilms")]
        public async Task<IActionResult> GetAllDislikedFilms()
        {
            return Ok(await _mediator.Send(new GetDislikedFilmsQuery()));
        }

        [HttpPost("Bookmark/{filmId}")]
        public async Task<IActionResult> BookmarkFilm(Guid filmId)
        {
            var result = await _mediator.Send(new BookmarkFilmCommand(filmId));
            if (!result.IsSuccessed)
                return Ok(result.Message);
            return Ok();
        }

        [HttpDelete("Bookmark/{filmId}")]
        public async Task<IActionResult> UnbookmarkFilm(Guid filmId)
        {
            var result = await _mediator.Send(new UnbookmarkFilmCommand(filmId));
            if (!result.IsSuccessed)
                return Ok(result.Message);
            return Ok();
        }

        [HttpGet("LikedBy/{userId}")]
        public async Task<IActionResult> GetLikedFilmsByUser(string userId)
        {
            Guid guidUserId;
            if (userId == "me")
            {
                var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (currentUserId == null) return Unauthorized();
                guidUserId = Guid.Parse(currentUserId);
            }
            else
            {
                if (!Guid.TryParse(userId, out guidUserId))
                    return BadRequest("Invalid userId");
            }

            var likedFilms = await _dbContext.UserLikedFilm
                .Where(x => x.UserId == guidUserId)
                .Include(x => x.Film).ThenInclude(f => f.Category)
                .Select(x => x.Film)
                .ToListAsync();

            var result = likedFilms.Select(f => new {
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
            });

            return Ok(result);
        }

        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations()
        {
            var result = await _mediator.Send(new GetRecommendationsQuery());
            return Ok(result);
        }

        [HttpGet("Bookmarked")]
        public async Task<IActionResult> GetBookmarkedFilms()
        {
            var result = await _mediator.Send(new GetBookmarkedFilmsQuery());
            return Ok(result.Films);
        }
    }
} 