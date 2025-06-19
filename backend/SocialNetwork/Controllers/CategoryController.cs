using FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories;
using FilmMatch.Application.Features.Categories.GetAllCategories;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpGet("GetCategory")]
        public async Task<ActionResult<IEnumerable<GetAllCategoriesDto>>> GetAllCategories()
        {
            var result = await mediator.Send(new GetAllCategoriesQuery());
            return Ok(result.Categories);
        }
    }

}

