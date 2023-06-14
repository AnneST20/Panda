using System.Collections.Generic;

namespace Panda.Library.Models.GeoJsonModels
{
    public class GeoJsonModel
    {
        public string type { get; set; }
        public List<Feature> features { get; set; }
    }
}
