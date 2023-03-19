using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Event {
    // Primary Key
    public int EventID {get; set;}

    // Properties
    public string Name {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}

    // Relationships

    // One-to-many
    public ICollection<Session> Sessions {get; set;}
    public ICollection<Speaker> Speakers {get; set;}
    public ICollection<Participant> Participants {get; set;}
    public ICollection<Paper> Papers {get; set;}

    // Many-to-many
    public ICollection<Sponsor> Sponsors {get; set;}
    public ICollection<Venue> Venues {get; set;}     
}
