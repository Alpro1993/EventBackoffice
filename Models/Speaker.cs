using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackoffice.Backend.Models;
public class Speaker : Person
{
    // Relationships

    // One-to-many
    public ICollection<Session> Sessions {get; set;} = new List<Session>();

    public Event Event {get; set;} = default!;
}