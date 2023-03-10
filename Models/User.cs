using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Panda.Models
{
    public class User : IdentityUser
    {
        public ICollection<LikedAd> LikedAds { get; set; }
    }
}