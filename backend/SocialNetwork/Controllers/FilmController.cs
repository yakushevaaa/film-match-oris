using System.ComponentModel.DataAnnotations;
using FilmMatch.Application.Features.Films.BookmarkFilm;
using FilmMatch.Application.Features.Films.CreateFilm;
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
using FilmMatch.Application.Features.Films.GetAllFilms;
using FilmMatch.Application.Features.Categories.GetAllCategories;
using FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories;
using FilmMatch.Application.Features.Films.DeleteFilm;
using FilmMatch.Application.Features.Films.UpdateFilm;

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
        [Consumes("multipart/form-data")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Create([FromForm] CreateFilmCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateFilmCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteFilmCommand(id));
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("GetAllFilms")]
        [Authorize]
        public async Task<IActionResult> GetAllFilms([FromQuery] Guid? categoryId = null, [FromQuery] string? search = null)
        {
            var result = await _mediator.Send(new GetAllFilmsQuery(categoryId, search));
            return Ok(result.Films);
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
        public async Task<IActionResult> GetAllDislikedFilms([FromQuery] Guid? userId = null)
        {
            return Ok(await _mediator.Send(new GetDislikedFilmsQuery(userId)));
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
        public async Task<IActionResult> UnBookmarkFilm(Guid filmId)
        {
            var result = await _mediator.Send(new UnbookmarkFilmCommand(filmId));
            if (!result.IsSuccessed)
                return Ok(result.Message);
            return Ok();
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