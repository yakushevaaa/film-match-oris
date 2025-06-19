using FilmMatch.Application.Features.Users.Commands.RegisterUser;
using FilmMatch.Application.Features.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FilmMatch.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FilmMatch.Domain.Constants;
using System.Security.Claims;
using FilmMatch.Application.Features.Users.Queries.GetUsernames;
using FilmMatch.Application.Features.Users.UserToAdmin;
using FilmMatch.Domain.Entities;
using FilmMatch.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Controllers ;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IDbContext _dbContext;

        public UserController(IMediator mediator, IUserService userService, UserManager<IdentityUser<Guid>> userManager, RoleManager<IdentityRole<Guid>> roleManager, IDbContext dbContext)
        {
            _mediator = mediator;
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
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
            var ok = await _mediator.Send(new LoginUserQuery()
            {
                Email = request.Email,
                Password = request.Password
            });
            return Ok(ok);

        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return NotFound();

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.IdentityUserId == identityUser.Id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(identityUser);

            return Ok(new
            {
                user.Id,
                user.Name,
                user.HasSubscription,
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
        
        [HttpGet("usernames")]
        [Authorize]
        public async Task<IActionResult> GetUsernames([FromQuery]string? query)
        {
            var result = await _mediator.Send(new GetUsernamesQuery(query));
            return Ok(result);
        }

        [HttpPut("UserToAdmin")]
        [Authorize(Roles = RoleConstants.God)]
        public async Task<IActionResult> UserToAdmin(Guid userId)
        {
            return Ok(await _mediator.Send(new UserToAdminCommand(userId)));
        }
    }