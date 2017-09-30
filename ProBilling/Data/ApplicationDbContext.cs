using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProBilling.Models;
using Microsoft.AspNetCore.Identity;

namespace ProBilling.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>()
                .HasKey(user => user.Id);

            builder.Entity<ApplicationUser>()
                .HasAlternateKey(user => user.UserName);

            builder.Entity<Team>()
                .HasKey(team => team.TeamId);

            builder.Entity<TeamUserMapping>()
                .HasKey(mapping => new { mapping.UserId, mapping.TeamId });

            builder.Entity<Sprint>()
                .HasKey(sprint => sprint.SprintId);

            builder.Entity<SprintActivity>()
                .HasKey(activity => activity.ActivityId);

            builder.Entity<SprintReport>()
                .HasKey(sprintReport => sprintReport.SprintReportId);

            //Sprint belongs to 1 team but 1 team can have many sprints
            builder.Entity<Sprint>()
                .HasOne(sprint => sprint.Team)
                .WithMany(team => team.Sprints)
                .HasForeignKey(sprint => sprint.TeamId);

            //Sprint Activity is per user per sprint
            //sprint will have a lot of activities
            builder.Entity<SprintActivity>()
                .HasOne(activity => activity.Sprint)
                .WithMany(sprint => sprint.SprintActivities)
                .HasForeignKey(activity => activity.SprintId);

            //Sprint Activity is per user per sprint
            //user will have a lot of activities
            builder.Entity<SprintActivity>()
                .HasOne(activity => activity.User)
                .WithMany(user => user.UserActivities)
                .HasForeignKey(activity =>activity.UserId);

            //Configuring many to many relationship
            builder.Entity<TeamUserMapping>()
                .HasOne(teamUserMapping => teamUserMapping.User)
                .WithMany(user => user.TeamUserMapping)
                .HasForeignKey(teamUserMapping => teamUserMapping.UserId);

            builder.Entity<TeamUserMapping>()
                .HasOne(teamUserMapping => teamUserMapping.Team)
                .WithMany(team => team.TeamUserMapping)
                .HasForeignKey(teamUserMapping => teamUserMapping.TeamId);

            builder.Entity<Sprint>()
                .HasOne(sprint => sprint.SprintReport)
                .WithOne(report => report.Sprint)
                .HasForeignKey<Sprint>(sprint => sprint.SprintReportId);
        }

        public DbSet<ProBilling.Models.Team> Team { get; set; }

        public DbSet<ProBilling.Models.Sprint> Sprint { get; set; }

        public DbSet<ProBilling.Models.SprintActivity> SprintActivity { get; set; }

        public DbSet<ProBilling.Models.TeamUserMapping> TeamUserMapping { get; set; }
    }
}
