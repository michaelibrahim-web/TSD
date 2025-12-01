using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
                // Employee + TimeEntry (1:M)
                modelBuilder.Entity<TimeEntry>()
                    .HasOne(t => t.Employee)
                    .WithMany(e => e.TimeEntries)
                    .HasForeignKey(t => t.EmployeeId);
                // Project + TimeEntry (1:M)
                modelBuilder.Entity<TimeEntry>()
                    .HasOne(t => t.Project)
                    .WithMany(p => p.TimeEntries)
                    .HasForeignKey(t => t.ProjectId);
            // Employee + Projects (1:M)
            modelBuilder.Entity<Project>()
             .HasOne(p => p.Lead)            
            .WithMany(e => e.LedProjects)     
            .HasForeignKey(p => p.LeadId);
            // Client + Project (1:M)
            modelBuilder.Entity<Project>()
                    .HasOne(p => p.Client)
                    .WithMany(c => c.Projects)
                    .HasForeignKey(p => p.ClientId);

                // Category + TimeEntry (1:M)
                modelBuilder.Entity<TimeEntry>()
                    .HasOne(t => t.Category)
                    .WithMany(cat => cat.TimeEntries)
                    .HasForeignKey(t => t.CategoryId);

                // Employee + TeamMember (1:M)
                modelBuilder.Entity<TeamMember>()
                    .HasOne(tm => tm.Employee)
                    .WithMany(e => e.TeamMembers)
                    .HasForeignKey(tm => tm.EmployeeId);
                // Project + TeamMember (1:M)
                modelBuilder.Entity<TeamMember>()
                    .HasOne(tm => tm.Project)
                    .WithMany(p => p.TeamMembers)
                    .HasForeignKey(tm => tm.ProjectId);
            }
        
    }
}
