using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EventBackofficeBackend.Models;
public class Paper
{
    // Primary Key
    public int PaperID {get; set;} = default;
    
    // Properties
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public string Title {get; set;} = default!;
    [Url]
    public string File {get; set;} = default!;
    
    // Relationships

    // One-to-one
    [Required]
    public Person MainAuthor {get; set;} = default!;
    public int MainAuthorID {get; set;} = default;
    public Person Presenter {get; set;} = default!;
    public int PresenterID {get; set;} = default;

    // Navigation
    public Event Event {get; set;} = default!;
    public int EventID {get; set;} = default;
    public Session Session {get; set;} = default!;
    public int SessionID {get; set;} = default;
}