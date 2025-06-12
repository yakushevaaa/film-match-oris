using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("check-image")]
    public IActionResult CheckImage()
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "category", "action.png");
        if (System.IO.File.Exists(imagePath))
        {
            return Ok(new { exists = true, path = imagePath });
        }
        return NotFound(new { exists = false, path = imagePath });
    }
} 