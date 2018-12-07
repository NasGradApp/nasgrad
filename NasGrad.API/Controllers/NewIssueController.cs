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

            var pictureName = newIssue.GetValue("pictureInfo").Value<string>("name");
            var pictureB64 = newIssue.GetValue("pictureInfo").Value<string>("rawData");

            return Ok("Issue added");
        }
    }
}
/*
var dbCollection = _database.GetCollection<NasGradPicture>(Constants.PictureTableName);
var pic = await dbCollection.Find(c => string.Equals(c.Id, id)).ToListAsync();
pic[0].Visible = visible;
                await dbCollection.InsertOneAsync(pic[0]);
                return true;*/