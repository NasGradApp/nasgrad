using System;
using System.Collections.Generic;
using System.Text;

namespace NasGrad.DBEngine
{
    public class NasGradType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
