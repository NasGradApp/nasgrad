using ImageMagick;
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
            issue.PicturePreview = ResizeImage(pict.Content, 128, 128);

            issue.LikedCount = 1;
            _dbStorage.InsertNewIssue(issue);

            return Ok("Issue added");
        }

        [HttpPut("LikeIssue")]
        public async Task<IActionResult> LikeIssue(string id)
        {
            if (await _dbStorage.UpdateIssueLike(id, 1))
            {
                return Ok("Issue updated");
            }

            return BadRequest();
        }

        [HttpPut("DislikeIssue")]
        public async Task<IActionResult> DislikeIssue(string id)
        {
            if (await _dbStorage.UpdateIssueDislike(id, 1))
            {
                return Ok("Issue updated");
            }

            return BadRequest();
        }

        private string ResizeImage(string b64Img, int width, int height)
        {
            try
            {
                using (var image = new MagickImage(Convert.FromBase64String(b64Img)))
                {
                    image.Resize(width, height);
                    image.Strip();
                    image.Quality = 50;
                    return image.ToBase64();
                }
            }
            catch (Exception ex)
            {
                return b64Img;
            }
        }
    }
}