using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.AuthorizationPolicyAdmin)]
    public class CRUDController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public CRUDController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        #region [CityService]

        // GET: api/GetAllCityServices
        [HttpGet("GetAllCityServices")]
        public async Task<IActionResult> GetAllCityServices()
        {
            var result = await _dbStorage.GetAllCityServices();
            return Ok(result);
        }

        // GET: api/GetCityService/5
        [HttpGet("GetCityService/{id}")]
        public async Task<IActionResult> GetCityService(string id)
        {
            var result = await _dbStorage.GetCityService(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCityService/{id}")]
        public async Task<IActionResult> DeleteCityService(string id)
        {
            var result = await _dbStorage.DeleteCityService(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("CreateCityService")]
        public async Task<IActionResult> CreateCityService([FromBody] NasGradCityService data)
        {
            var result = await _dbStorage.CreateCityService(data);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateCityService")]
        public async Task<IActionResult> UpdateCityService([FromBody] NasGradCityService data)
        {
            var result = await _dbStorage.UpdateCityService(data);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Region

        // GET: api/GetAllRegions
        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var result = await _dbStorage.GetAllRegions();
            return Ok(result);
        }

        // GET: api/GetRegion/5
        [HttpGet("GetRegion/{id}")]
        public async Task<IActionResult> GetRegion(string id)
        {
            var result = await _dbStorage.GetRegion(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteRegion/{id}")]
        public async Task<IActionResult> DeleteRegion(string id)
        {
            var result = await _dbStorage.DeleteRegion(id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("CreateRegion")]
        public async Task<IActionResult> CreateRegion([FromBody] NasGradRegion data)
        {
            var result = await _dbStorage.CreateRegion(data);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateRegion")]
        public async Task<IActionResult> UpdateRegion([FromBody] NasGradRegion data)
        {
            var result = await _dbStorage.UpdateRegion(data);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region Type

        // GET: api/GetAllTypes
        [HttpGet("GetAllTypes")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _dbStorage.GetAllTypes();
            return Ok(result);
        }

        // GET: api/GetType/5
        [HttpGet("GetType/{id}")]
        public async Task<IActionResult> GetType(string id)
        {
            var result = await _dbStorage.GetNasGradType(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(string id)
        {
            var result = await _dbStorage.DeleteType(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("CreateType")]
        public async Task<IActionResult> CreateType([FromBody] NasGradType data)
        {
            var result = await _dbStorage.CreateType(data);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateType")]
        public async Task<IActionResult> UpdateType([FromBody] NasGradType data)
        {
            var result = await _dbStorage.UpdateType(data);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion

        #region CityServiceType

        // GET: api/GetAllCityServiceTypes
        [HttpGet("GetAllCityServiceTypes")]
        public async Task<IActionResult> GetAllCityServiceTypes()
        {
            var result = await _dbStorage.GetAllCityServiceTypes();
            return Ok(result);
        }

        // GET: api/GetCityServiceType/5
        [HttpGet("GetCityServiceType/{id}")]
        public async Task<IActionResult> GetCityServiceType(string id)
        {
            var result = await _dbStorage.GetNasGradCityServiceType(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteCityServiceType/{id}")]
        public async Task<IActionResult> DeleteCityServiceType(string id)
        {
            var result = await _dbStorage.DeleteCityServiceType(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("CreateCityServiceType")]
        public async Task<IActionResult> CreateCityServiceType([FromBody] NasGradCityServiceType data)
        {
            var result = await _dbStorage.CreateCityServiceType(data);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("UpdateCityServiceType")]
        public async Task<IActionResult> UpdateCityServiceType([FromBody] NasGradCityServiceType data)
        {
            var result = await _dbStorage.UpdateCityServiceType(data);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        #endregion
    }
}
