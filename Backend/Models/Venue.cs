using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackoffice.Backend.Models;
public class Venue
{
    // Primary Key
    public int VenueID {get; set;}

    // Properties
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name {get; set;} = default!;
    [Url]
    public string Location {get; set;} = default!;

    //Relationships

    //One-to-many
    public ICollection<Room> Rooms = new List<Room>();
    public ICollection<Event> Events = new List<Event>();
    public ICollection<Session> Sessions = new List<Session>();      
}