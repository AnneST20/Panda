using System.Collections.Generic;

namespace Panda.Library.Models.GeoJsonModels
{
    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
}
