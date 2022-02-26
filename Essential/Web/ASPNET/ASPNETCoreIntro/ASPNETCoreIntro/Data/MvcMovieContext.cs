using ASPNETCoreIntro.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreIntro.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
           : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
