using Microsoft.EntityFrameworkCore;
using TSD.Domain.Entities;

namespace TSD.Data
{
    public class TSD_DbContext : DbContext
    {
        public TSD_DbContext(DbContextOptions<TSD_DbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<TimeEntry>(entity =>
            {
                entity.Property(te => te.Hours)
                      .HasPrecision(18, 2); 
                entity.Property(te => te.OverTime)
                      .HasPrecision(18, 2); 

                // Relationships
                entity.HasOne(te => te.Project)
                      .WithMany(p => p.TimeEntries)
                      .HasForeignKey(te => te.ProjectId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(te => te.Employee)
                      .WithMany(e => e.TimeEntries)
                      .HasForeignKey(te => te.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(te => te.Category)
                      .WithMany(c => c.TimeEntries)
                      .HasForeignKey(te => te.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

           
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasOne(p => p.Lead)
                      .WithMany(e => e.LedProjects)
                      .HasForeignKey(p => p.LeadId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(p => p.Client)
                      .WithMany(c => c.Projects)
                      .HasForeignKey(p => p.ClientId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            
            modelBuilder.Entity<TeamMember>(entity =>
            {
                entity.HasOne(tm => tm.Employee)
                      .WithMany(e => e.TeamMembers)
                      .HasForeignKey(tm => tm.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(tm => tm.Project)
                      .WithMany(p => p.TeamMembers)
                      .HasForeignKey(tm => tm.ProjectId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            
        }
    }
}
