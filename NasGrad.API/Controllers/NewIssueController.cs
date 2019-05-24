using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using NasGrad.API.DTO;
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

        [HttpPost("NewIssue")]
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

        /// <summary>
        /// Creates a new issue
        /// </summary>
        /// <remarks>
        /// PictureInfo Content is a base64 encoded string of the picture byte data
        ///
        /// </remarks>
        [HttpPost("NewIssueV2")]
        public ActionResult NewIssueV2([FromBody] NewIssueDTO newIssue)
        {
            var issue = newIssue.Issue;
            var pict = newIssue.PictureInfo;

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