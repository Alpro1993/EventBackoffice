using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Person
{
    // Primary Key
    public int PersonID {get; set;}
    
    // Properties
    [Required]
    [StringLength(747, MinimumLength = 2)]
    public string Name {get; set;} = default!;
    [Url]
    public string Picture {get; set;} = default!;
    [StringLength(747, MinimumLength = 2)]
    public string Bio {get; set;} = default!;
    [EmailAddress]
    public string Email {get; set;} = default!;

    // Relationships

    // One-to-many
    public ICollection<Paper> Papers = new List<Paper>();
    public ICollection<Paper> Presenting = new List<Paper>();
}