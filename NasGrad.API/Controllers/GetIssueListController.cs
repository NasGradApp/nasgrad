using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using System.Collections.Generic;
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
        public async Task<NasGradIssue> Get(string id)
        {
            return await _dbStorage.GetIssue(id);
        }

        [HttpGet]
        public async Task<List<NasGradIssue>> Get()
        {
            return await _dbStorage.GetIssues();
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