using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NasGrad.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewIssueController : ControllerBase
    {
        private IDBStorage _dbStorage;

        [HttpPost]
        public async Task<IActionResult> NewIssue([FromBody] JObject newIssue)
        {
            var issue = new NasGradIssue();
            return Ok("Issue added");
        }
    }

    public class IssueLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }


    public class NasGradIssue
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string IssueType { get; set; }
        public StateEnum State { get; set; }
        public List<string> Pictures { get; set; }
        public List<string> Categories { get; set; }
        public IssueLocation Location { get; set; }
        public byte[] PicturePreview { get; set; }
    }

    public enum StateEnum
    {
        Submitted = 1,
        Reported = 2,
        Done = 3
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
