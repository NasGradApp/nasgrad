using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public CategoryController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _dbStorage.GetCategories();
            return Ok(result);
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _dbStorage.GetCategory(id);
            return Ok(result);
        }

        //api/Category/GetSelectedCategories?ids=cat1&ids=cat2
        [HttpGet("GetSelectedCategories")]
        public async Task<IActionResult> GetSelectedCategories(string[] ids)
        {
            var result = await _dbStorage.GetSelectedCategories(ids);
            return Ok(result);
        }

        //// POST: api/Category
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Category/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
