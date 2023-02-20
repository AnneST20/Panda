using System.Collections.Generic;

namespace Panda.Models
{
    public class Ad
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Price { get; set; }
        public string Adress { get; set; }
        public string Rooms { get; set; }
        public string Floor { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public List<string> Gallery { get; set; }
    }
}