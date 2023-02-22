using System.Collections.Generic;

namespace Panda.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Ad> Ads { get; set; }
    }
}