using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClubProject1.Core.Entities;

namespace VideoClubProject1.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<PhysicalCopy> PhysicalCopies { get; set; }
        //public IEnumerable<object> Users { get; set; }

        public ApplicationDbContext()
            : base("VideoClubDbContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<VideoClubProject1.Core.Entities.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<VideoClubProject.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
