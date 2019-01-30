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
            var result = await _dbStorage.GetAllIssuesForApproval();
            return Ok(result);
        }

        [HttpGet("GetIssueDetails/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _dbStorage.GetIssue(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("approveItem")]
        public async Task<IActionResult> ApproveItem([FromBody]string id)
        {
            var result = await _dbStorage.ApproveIssue(id);
            return Ok(result);
        }

        [HttpDelete("deleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody]string id)
        {
            var result = await _dbStorage.DeleteIssue(id);
            return Ok(result);
        }
    }
}