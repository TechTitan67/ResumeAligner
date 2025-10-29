using Microsoft.EntityFrameworkCore;
using ResumeAligner.Entities;

namespace ResumeAligner.Data
{
    public class ResumeAlignerDbContext : DbContext
    {
        public ResumeAlignerDbContext(DbContextOptions<ResumeAlignerDbContext> options)
            : base(options) { }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<JobDescription> JobDescriptions { get; set; }
        public DbSet<JobMatch> JobMatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobMatch>()
                .HasOne(jm => jm.Resume)
                .WithMany(r => r.Matches)
                .HasForeignKey(jm => jm.ResumeId);

            modelBuilder.Entity<JobMatch>()
                .HasOne(jm => jm.JobDescription)
                .WithMany(jd => jd.Matches)
                .HasForeignKey(jm => jm.JobDescriptionId);
        }
    }
}
