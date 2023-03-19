using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Paper
{
    // Primary Key
    public int PaperID {get; set;}
    
    // Properties
    [StringLength(150, MinimumLength = 1)]
    public required string Title {get; set;}
    [Url]
    public string File {get; set;}
    
    // Relationships

    // One-to-one
    public Person MainAuthor {get; set;}
    public Person Presenter {get; set;}

    // Navigation
    public Event Event {get; set;}
    public Session Session {get; set;}
}