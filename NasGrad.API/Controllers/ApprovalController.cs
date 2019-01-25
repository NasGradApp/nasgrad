using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Constants.AuthorizationPolicyAdmin)]
    public class ApprovalController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public ApprovalController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        [HttpGet("allIssues")]
        public async Task<IActionResult> Get()
        {
            var result = await _dbStorage.GetAllIssues();
            return Ok(result);
        }
    }
}