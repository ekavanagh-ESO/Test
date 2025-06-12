using Microsoft.EntityFrameworkCore;
using MythTechTest.Models.Entities;
namespace MythTechTest.Data

{
    public class SportsEventContext : DbContext
    {
        public SportsEventContext(DbContextOptions<SportsEventContext> options)
            : base(options)
        {
        }

        public DbSet<SportsEvent> SportsEvents { get; set; }
        public DbSet<EventState> EventStates { get; set; }
        public DbSet<SportsOrganization> SportsOrganizations { get; set; }
        public DbSet<ParentEvent> ParentEvents { get; set; }
        public DbSet<RelatedEvent> RelatedEvents { get; set; }
        public DbSet<EventMeta> EventMetas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelatedEvent>()
                .OwnsOne(re => re.NavigationInfo);
            
            modelBuilder.Entity<SportsEvent>()
                .HasOne(e => e.Meta)
                .WithOne(m => m.SportsEvent)
                .HasForeignKey<EventMeta>(m => m.SportsEventId);
            
            modelBuilder.Entity<EventState>()
                .HasOne(es => es.SportsEvent)
                .WithMany(se => se.States)
                .HasForeignKey(es => es.SportsEventId);

            modelBuilder.Entity<SportsOrganization>()
                .HasOne(so => so.SportsEvent)
                .WithMany(se => se.SportsOrganizations)
                .HasForeignKey(so => so.SportsEventId);

            modelBuilder.Entity<ParentEvent>()
                .HasOne(pe => pe.SportsEvent)
                .WithMany(se => se.ParentEvents)
                .HasForeignKey(pe => pe.SportsEventId);

            modelBuilder.Entity<RelatedEvent>()
                .HasOne(re => re.SportsEvent)
                .WithMany(se => se.RelatedEvents)
                .HasForeignKey(re => re.SportsEventId);
        }
    }
}

