using System;
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
        public string Coordinates { get; set; }
        [Required]
        public string Square { get; set; }
        [Required]
        public string Rooms { get; set; }
        [Required]
        public string Floor { get; set; }
        public string Description { get; set; }
        public bool PetsAllowed { get; set; }
        public bool ChildrenAllowed { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime SaveToContextDate { get; set; }
        public List<Photo> Gallery { get; set; }
        public List<LikedAd> LikedAds { get; set; }
    }
}