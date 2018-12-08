using Microsoft.AspNetCore.Mvc;
using NasGrad.DBEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using ImageMagick;

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
            _dbStorage.InsertNewIssue(issue);

            return Ok("Issue added");
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