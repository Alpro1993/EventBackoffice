using System.ComponentModel.DataAnnotations;

namespace EventBackofficeBackend.Models;
public class Room {
    public int RoomID {get; set;}
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name {get; set;} = default!;

    [Required]
    public Venue Venue {get; set;} = default!;
    public int VenueID {get; set;} = default;
    public ICollection<Session> Sessions {get; set;} = new List<Session>();
}
