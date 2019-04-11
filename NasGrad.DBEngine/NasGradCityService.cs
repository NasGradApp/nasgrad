using System;
using System.Collections.Generic;
using System.Text;

namespace NasGrad.DBEngine
{
    public class NasGradCityService: BaseItem
    {
        public string Region { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}