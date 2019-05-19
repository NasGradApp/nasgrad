using NasGrad.DBEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NasGrad.API.DTO
{
    public class NewIssueDTO
    {
        [JsonProperty("issue")]
        public NasGradIssue Issue { get; set; }

        [JsonProperty("pictureInfo")]
        public NasGradPicture PictureInfo { get; set; }
    }
}
