using FilmMatch.Application.Features.Users.Commands.RegisterUser;
using FilmMatch.Application.Features.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FilmMatch.Domain.Constants;

namespace FilmMatch.Controllers ;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public UserController(IMediator mediator, IUserService userService, UserManager<IdentityUser<Guid>> userManager)
        {
            _mediator = mediator;
            _userService = userService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand()
            {
                Password = request.Password,
                Email = request.Email,
                Name = request.Name
            }));
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery request)
        {
            return Ok(await _mediator.Send(new LoginUserQuery()
            {
                Email = request.Email,
                Password = request.Password
            }));
        }

        public class MakeAdminRequest
        {
            public string Email { get; set; }
        }

        [Authorize(Roles = RoleConstants.God)]
        [HttpPost("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] MakeAdminRequest request)
        {
            var result = await _userService.MakeAdminAsync(request.Email);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Admin rights granted");
        }
    }