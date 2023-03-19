namespace EventBackofficeBackend.Models;
public class Room {
    public int RoomID {get; set;}
    public string Name {get; set;}

    public Venue Venue {get; set;}
    public ICollection<Session> Sessions {get; set;}
}
