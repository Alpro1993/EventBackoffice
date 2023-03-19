using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Session
{
    // Primary Key
    public int SessionID {get; set;}

    // Properties
    public string Name {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}
    
    // Relationships

    // Many-to-one
    public Room Room {get; set;}
    public Event Event {get; set;}
    public int EventID {get; set;}
    public Venue Venue {get; set;}
    public int VenueID {get; set;}

    // Many-to-many
    public ICollection<Sponsor> Sponsors {get; set;}
    public ICollection<Speaker> Speakers {get; set;}
    public ICollection<Participant> Participants {get; set;}

    // One-to-Many
    public ICollection<Paper> Papers {get; set;}

    // Hierarchical
    public Session parentSession {get; set;}
}
