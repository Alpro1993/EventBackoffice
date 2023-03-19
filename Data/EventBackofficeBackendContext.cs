using Microsoft.EntityFrameworkCore;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Data;
public class EventBackofficeBackendContext : DbContext
{
    public EventBackofficeBackendContext (DbContextOptions<EventBackofficeBackendContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Speaker> Speakers {get; set;}
    public DbSet<Paper> Papers {get; set;}
    public DbSet<Participant> Participants {get; set;}
    public DbSet<Session> Sessions {get; set;}
    public DbSet<Sponsor> Sponsors {get; set;}
    public DbSet<Venue> Venues {get; set;}
    public DbSet<Room> Rooms {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Event>()
            .HasMany(s => s.Sessions)
            .WithOne(e => e.Event);
        modelBuilder.Entity<Event>()
            .HasMany(s => s.Sponsors)
            .WithMany(e => e.Events);
        modelBuilder.Entity<Event>()
            .HasMany(v => v.Venues)
            .WithMany(e => e.Events);
        modelBuilder.Entity<Event>()
            .HasMany(s => s.Speakers)
            .WithOne(e => e.Event);
        modelBuilder.Entity<Event>()
            .HasMany(p => p.Participants)
            .WithOne(e => e.Event);
        modelBuilder.Entity<Event>()
            .HasMany(p => p.Papers)
            .WithOne(e => e.Event);

        modelBuilder.Entity<Session>()
            .HasMany(s => s.Speakers)
            .WithMany(s => s.Sessions);
        modelBuilder.Entity<Session>()
            .HasMany(p => p.Papers)
            .WithOne(s => s.Session);
        modelBuilder.Entity<Session>()
            .HasMany(s => s.Sponsors)
            .WithMany(s => s.Sessions);
        modelBuilder.Entity<Session>()
            .HasMany(p => p.Participants)
            .WithMany(s => s.Sessions);
        modelBuilder.Entity<Session>()
            .HasOne(r => r.Room)
            .WithMany(s => s.Sessions);
        modelBuilder.Entity<Session>()
            .HasOne(v => v.Venue)
            .WithMany(s => s.Sessions);
        
        modelBuilder.Entity<Venue>()
            .HasMany(r => r.Rooms)
            .WithOne(v => v.Venue);
        
        modelBuilder.Entity<Paper>()
            .HasOne(a => a.MainAuthor)
            .WithMany(p => p.Papers);
        modelBuilder.Entity<Paper>()
            .HasOne(p => p.Presenter)
            .WithMany(p => p.Presenting);
            
    }
}
