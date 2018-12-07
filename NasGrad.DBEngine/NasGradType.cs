using System.Collections.Generic;

namespace NasGrad.DBEngine
{
    public class NasGradType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; }
    }
}
