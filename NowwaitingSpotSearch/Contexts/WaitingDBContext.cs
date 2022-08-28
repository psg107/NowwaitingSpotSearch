using Microsoft.EntityFrameworkCore;
using NowwaitingSpotSearch.Entities;

namespace NowwaitingSpotSearch.Contexts
{
    public class WaitingDBContext : DbContext
    {
        public WaitingDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<SpotEntity> Spots { get; set; }

        public DbSet<AuthTokenEntity> AuthTokens { get; set; }
    }
}
