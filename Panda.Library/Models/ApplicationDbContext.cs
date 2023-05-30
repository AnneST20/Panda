using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace Panda.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Ad> Ads { get; set; }
        public DbSet<Photo> Gallery { get; set; }
        public DbSet<LikedAd> LikedAds { get; set; }

        public ApplicationDbContext()
            : base("PandaConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }


}