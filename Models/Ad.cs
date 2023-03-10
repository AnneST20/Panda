using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Panda.Models
{
    public class Ad
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string SourceKey { get; set; }
        public string Url { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string AdressLink { get; set; }
        [Required]
        public string Square { get; set; }
        [Required]
        public string Rooms { get; set; }
        [Required]
        public string Floor { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool PetsAllowed { get; set; }
        public List<Photo> Gallery { get; set; }
        public List<LikedAd> LikedAds { get; set; }
    }
}