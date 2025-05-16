using FilmMatch.Models;
using FilmMatch.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace FilmMatch.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetCategory")]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _dbContext.Categories.ToList();
            return Ok(categories);
        }


        //public ActionResult<IEnumerable<Category>> GetAll()
        //=> Ok(_dbContext.Cars.Select(x => new CarVm(x)));

        //[HttpGet("GetCarById/{id:int}")]
        //public Car? GetById([FromRoute] int id)
        //    => _dbContext.Cars.FirstOrDefault(x => x.Id == id);



    }
    
}
