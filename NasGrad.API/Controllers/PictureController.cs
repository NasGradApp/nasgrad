using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public PictureController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        // GET: api/Picture
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _dbStorage.GetPictures();
            return Ok(result);
        }

        // GET: api/Picture/5
        [HttpGet("{id}", Name = "GetPicture")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _dbStorage.GetPicture(id);
            return Ok(result);
        }

        //// POST: api/Picture
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Picture/5
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
