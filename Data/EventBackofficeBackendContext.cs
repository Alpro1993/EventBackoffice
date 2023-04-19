using Microsoft.EntityFrameworkCore;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Data;
public class BackendContext : DbContext
{
    public BackendContext (DbContextOptions<BackendContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Speaker> Speakers {get; set;} = default!;
    public DbSet<Paper> Papers {get; set;} = default!;
    public DbSet<Participant> Participants {get; set;} = default!;
    public DbSet<Session> Sessions {get; set;} = default!;
    public DbSet<Sponsor> Sponsors {get; set;} = default!;
    public DbSet<Venue> Venues {get; set;} = default!;
    public DbSet<Room> Rooms {get; set;} = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Event>()
            .HasMany(s => s.Sponsors)
            .WithMany(e => e.Events);
        modelBuilder.Entity<Event>()
            .HasMany(v => v.Venues)
            .WithMany(e => e.Events);


        modelBuilder.Entity<Session>()
            .HasOne(e => e.Event)
            .WithMany(s => s.Sessions)
            .IsRequired();
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
        modelBuilder.Entity<Session>()
            .HasOne(p => p.parentSession)
            .WithMany(c => c.childSessions)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<Room>()
            .HasOne(v => v.Venue)
            .WithMany(r => r.Rooms)
            .IsRequired();

        
        modelBuilder.Entity<Paper>()
            .HasOne(e => e.Event)
            .WithMany(p => p.Papers)
            .IsRequired();
        modelBuilder.Entity<Paper>()
            .HasOne(a => a.MainAuthor)
            .WithMany(p => p.Papers)
            .IsRequired();
        modelBuilder.Entity<Paper>()
            .HasOne(p => p.Presenter)
            .WithMany(p => p.Presenting);


        modelBuilder.Entity<Speaker>()
            .HasOne(e => e.Event)
            .WithMany(s => s.Speakers)
            .IsRequired();


        modelBuilder.Entity<Participant>()
            .HasOne(e => e.Event)
            .WithMany(p => p.Participants)
            .IsRequired();

        
        modelBuilder.Entity<Representative>()
            .HasOne(s => s.Sponsor)
            .WithMany(r => r.Representatives)
            .IsRequired();
            
    }
}
