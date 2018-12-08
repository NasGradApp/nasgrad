using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetIssueListController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public GetIssueListController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        [HttpGet("GetIssueDetails/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _dbStorage.GetIssue(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _dbStorage.GetIssues();
            return Ok(result);
        }

        [HttpPut("UpdateIssueStatus")]
        public async Task<IActionResult> UpdateIssueStatus(string id, int statusId)
        {
            var result = await _dbStorage.UpdateIssueStatus(id, statusId);
            return Ok(result);
        }

    }
}

/*
Issue
{
    "id" : GUID,
    "owner-id": GUID, //citizen guid
    
    "title": string (100),
    "description": string,

    "issue-type": string, //predefined enum
    "categories": [string], //enum category
    "pictures": [picture-id-mdb], //id of picture in mongo db
    "state": string //enum of states

    "location": {
        "langitude": double,
        "longitude": double
    }
}

/api/getIssueList
{
    "count": int,
    "issues": [
        {
            "issue": Issue,
            "picture-preview": byte[]
        }
    ]
}
*/