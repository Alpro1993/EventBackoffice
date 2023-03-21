using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Sponsor
{
    //Primary Key
    public int SponsorID {get; set;}

    //Properties
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name {get; set;} = default!;
    [Url]
    public string Logo {get; set;} = default!;
    public int FloorSpace {get; set;} = default;
    [Url]
    public string URL {get; set;} = default!;

    //Relationships

    //One-to-many
    public ICollection<Representative> Representatives {get; set;} = new List<Representative>();
    public ICollection<Session> Sessions {get; set;} = new List<Session>();

    //Many-to-many
    public ICollection<Event> Events {get; set;} = new List<Event>();
}