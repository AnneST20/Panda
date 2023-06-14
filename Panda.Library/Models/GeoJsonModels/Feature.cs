namespace Panda.Library.Models.GeoJsonModels
{
    public class Feature
    {
        public Geometry geometry { get; set; }
        public string type { get; set; }
        public Properties properties { get; set; }
    }
}
