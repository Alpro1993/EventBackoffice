using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackoffice.Backend.Models;
public class Event {
    // Primary Key
    public int EventID {get; set;}

    // Properties
    [Required]
    [StringLength(747, MinimumLength = 2)]
    public string Name {get; set;} = default!;
    [Required]
    public DateTime StartDate {get; set;}
    [Required]
    public DateTime EndDate {get; set;}

    // Relationships

    // One-to-many
    public ICollection<Session> Sessions {get; set;} = new List<Session>();
    public ICollection<Speaker> Speakers {get; set;} = new List<Speaker>();
    public ICollection<Participant> Participants {get; set;} = new List<Participant>();
    public ICollection<Paper> Papers {get; set;} = new List<Paper>();

    // Many-to-many
    public ICollection<Sponsor> Sponsors {get; set;} = new List<Sponsor>();
    public ICollection<Venue> Venues {get; set;} = new List<Venue>();     
}
