using FilmMatch.Application.Contracts.Responses.Categories.GetAllCategories;
using FilmMatch.Application.Features.Categories.GetAllCategories;
using FilmMatch.Application.Features.Categories.CreateCategory;
using FilmMatch.Application.Features.Categories.UpdateCategory;
using FilmMatch.Application.Features.Categories.DeleteCategory;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Constants;
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

        [HttpPost]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var id = await mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest();
            var result = await mediator.Send(command);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteCategoryCommand(id));
            if (!result)
                return NotFound();
            return NoContent();
        }
    }

}

