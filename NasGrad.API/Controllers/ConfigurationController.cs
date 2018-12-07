using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public ConfigurationController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        // GET: api/Configuration
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _dbStorage.GetConfiguration();
            return Ok(result);
        }

        //// GET: api/Configuration/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Configuration
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Configuration/5
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
