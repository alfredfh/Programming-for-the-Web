using Microsoft.EntityFrameworkCore;
using VoteForCauseFinal.Models.Domain;

namespace VoteForCauseFinal.Data
{
    public class VoteDbContext : DbContext
    {
        public VoteDbContext(DbContextOptions<VoteDbContext> options) : base(options) 
        { 
        }

        public DbSet<CausePost> CausePosts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<CausePostSign> CausePostSigns { get; set; }

        public DbSet<CausePostComment> CausePostComment { get; set; }


    }
}
