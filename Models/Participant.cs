using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackoffice.Backend.Models;
public class Participant : Person
{
    // Properties
    [Required]
    public bool Visible {get; set;} = default!;

    // Reference Navigation Property
    public int EventID {get; set;} = default;
    public ICollection<Session> Sessions {get; set;} = new List<Session>();

    public Event Event {get; set;} = default!;

}