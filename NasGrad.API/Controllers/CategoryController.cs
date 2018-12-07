using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Collections.Generic;
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
        public async Task<List<NasGradCategory>> Get()
        {
            return await _dbStorage.GetCategories();
        }

        // GET: api/Category/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<NasGradCategory> Get(string id)
        {
            return await _dbStorage.GetCategory(id);
        }

        //api/Category/GetSelectedCategories?ids=cat1&ids=cat2
        [HttpGet("GetSelectedCategories")]
        public async Task<List<NasGradCategory>> GetSelectedCategories(string[] ids)
        {
            return await _dbStorage.GetSelectedCategories(ids);
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
