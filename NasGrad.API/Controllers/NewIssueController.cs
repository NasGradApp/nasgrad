using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using Newtonsoft.Json;
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

        public NewIssueController(IDBStorage dBStorage)
        {
            _dbStorage = dBStorage;
        }

        [HttpPost]
        public ActionResult NewIssue([FromBody] JObject newIssue)
        {
            var issue = JsonConvert.DeserializeObject<NasGradIssue>(newIssue.GetValue("issue").ToString());
            var pict = JsonConvert.DeserializeObject<NasGradPicture>(newIssue.GetValue("pictureInfo").ToString());
            pict.Visible = false;

            _dbStorage.InsertPicture(pict);
            issue.Pictures = new List<string> { pict.Id };
            issue.PicturePreview = pict.Content;
            _dbStorage.InsertNewIssue(issue);

            return Ok("Issue added");
        }
    }
}