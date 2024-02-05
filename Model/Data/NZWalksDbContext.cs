using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Model.Domain;

namespace NZWalks.Api.Model.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
