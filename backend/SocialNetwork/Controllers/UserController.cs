using FilmMatch.Application.Features.Users.Commands.RegisterUser;
using FilmMatch.Application.Features.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers ;

    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request)
        {
            return Ok(await mediator.Send(new RegisterUserCommand()
            {
                Password = request.Password,
                Email = request.Email,
                Name = request.Name
            }));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery request)
        {
            return Ok(await mediator.Send(new LoginUserQuery()
            {
                Email = request.Email,
                Password = request.Password
            }));
        }
    }