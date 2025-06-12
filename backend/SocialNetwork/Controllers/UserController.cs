using FilmMatch.Application.Features.Users.Commands.RegisterUser;
using FilmMatch.Application.Features.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FilmMatch.Domain.Constants;
using System.Security.Claims;

namespace FilmMatch.Controllers ;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UserController(IMediator mediator, IUserService userService, UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _mediator = mediator;
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
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

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                Roles = roles
            });
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<object>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new
                {
                    user.Id,
                    user.Email,
                    Roles = roles
                });
            }

            return Ok(userDtos);
        }

        [HttpPost("MakeAdmin/{userId}")]
        [Authorize(Roles = RoleConstants.God)]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await _userManager.AddToRoleAsync(user, RoleConstants.Admin);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("BlockUser/{userId}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> BlockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await _userManager.AddToRoleAsync(user, RoleConstants.Blocked);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("UnblockUser/{userId}")]
        [Authorize(Roles = $"{RoleConstants.God},{RoleConstants.Admin}")]
        public async Task<IActionResult> UnblockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var result = await _userManager.RemoveFromRoleAsync(user, RoleConstants.Blocked);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }
    }