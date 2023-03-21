using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Session
{
    // Primary Key
    public int SessionID {get; set;}

    // Properties
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name {get; set;} = default!;
    [Required]
    public DateTime StartDate {get; set;}
    [Required]
    public DateTime EndDate {get; set;}
    
    // Relationships

    // Many-to-one
    public Room Room {get; set;} = default!;
    public int RoomID {get; set;} = default;
    public Event Event {get; set;} = default!;
    public int EventID {get; set;} = default;
    public Venue Venue {get; set;} = default!;
    public int VenueID {get; set;} = default;


    // Many-to-many
    public ICollection<Sponsor> Sponsors {get; set;} = new List<Sponsor>();
    public ICollection<Speaker> Speakers {get; set;} = new List<Speaker>();
    public ICollection<Participant> Participants {get; set;} = new List<Participant>();

    // One-to-Many
    public ICollection<Paper> Papers {get; set;} = new List<Paper>();

    // Hierarchical
    public Session parentSession {get; set;} = default!;
    public int parentSessionID {get; set;} = default;
}
