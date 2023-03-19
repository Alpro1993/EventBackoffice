using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Sponsor
{
    //Primary Key
    public int SponsorID {get; set;}

    //Properties
    public required string Name {get; set;}
    [Url]
    public string Logo {get; set;}
    public int FloorSpace {get; set;}
    [Url]
    public string URL {get; set;}

    //Relationships

    //One-to-many
    public ICollection<Person> Representatives {get; set;}
    public ICollection<Session> Sessions {get; set;}

    //Many-to-many
    public ICollection<Event> Events {get; set;}
}