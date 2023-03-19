using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Speaker : Person
{
    // Relationships

    // One-to-many
    public ICollection<Session> Sessions {get; set;}   

    public Event Event {get; set;}
}