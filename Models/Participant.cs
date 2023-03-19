using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Participant : Person
{
    // Properties
    public bool Visible {get; set;}

    // Reference Navigation Property
    public int EventID {get; set;}
    public ICollection<Session> Sessions {get; set;}

    public Event Event {get; set;}
}