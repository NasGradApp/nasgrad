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
    public class GetIssueListController : ControllerBase
    {
        private IDBStorage _dbStorage;

        public GetIssueListController(IDBStorage dbStorage)
        {
            _dbStorage = dbStorage;
        }

        [HttpGet("GetIssueDetails/{id}")]
        public async Task<NasGradIssueWrapper> Get(string issueId)
        {
            return await _dbStorage.GetIssue(issueId);
        }

        [HttpPost("newIssue")]
        public async Task<IActionResult> Post([FromBody] NasGradIssueWrapper data)
        {
            try
            {
                var model = new 
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<List<NasGradIssueWrapper>> Get()
        {
            return await _dbStorage.GetIssues();
        }

        private string GetIssues()
        {
            var issues = new JObject();

            return dummy();
        }

        private string dummy()
        {

            var issue1 = new JObject
            {
                {"id", Guid.NewGuid()},
                {"owner-id", Guid.NewGuid()},
                {"title", "Rupa u putu na Trifkovic Trgu"},
                {"description", "to je to nemam sta drugo da dodam"},
                {"issue-type", "Rupa u putu"},
                {"state", "submited"}
            };

            JArray pict = new JArray();
            pict.Add("pic-1");
            pict.Add("pic-2");
            issue1.Add("pictures", pict);

            JArray cat = new JArray();
            cat.Add("JKP Put");
            cat.Add("JKP Zelenilo");
            issue1.Add("categories", cat);

            var loc = new JObject
            {
                {"langitude",  45.256950},
                {"longitude", 19.844151}
            };

            issue1.Add("location", loc);


            var issue2 = new JObject
            {
                {"id", Guid.NewGuid()},
                {"owner-id", Guid.NewGuid()},
                {"title", "Rupa koja nije na Trifkovic Trgu"},
                {"description", "to je to nemam sta drugo da dodam"},
                {"issue-type", "Rupa u putu"},
                {"state", "submited"}
            };

            pict = new JArray
            {
                "pic-3",
                "pic-4"
            };
            issue2.Add("pictures", pict);

            cat = new JArray
            {
                "JKP Put",
                "JKP Zelenilo"
            };
            issue2.Add("categories", cat);

            loc = new JObject
            {
                {"langitude",  46.256950},
                {"longitude", 20.844151}
            };

            issue2.Add("location", loc);

            var issues = new JObject();

            JArray isss = new JArray();

            var pic1 = new byte[] { 0x0, 0x1, 0x2 };//System.IO.File.ReadAllBytes("./test_prev.jpg");

            isss.Add(new JObject{
                {"issue",issue1 },
                {"picture-preview", pic1 },
            });


            isss.Add(new JObject{
                {"issue",issue2 },
                {"picture-preview", pic1 },
            });


            issues.Add("count", 2);
            issues.Add("issues", isss);
            return issues.ToString();
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