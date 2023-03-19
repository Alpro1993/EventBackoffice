using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Venue
{
    // Primary Key
    public int VenueID {get; set;}

    // Properties
    public required string Name {get; set;}
    [Url]
    public string Location {get; set;}

    //Relationships

    //One-to-many
    public ICollection<Room> Rooms;
    public ICollection<Event> Events;
    public ICollection<Session> Sessions;      
}