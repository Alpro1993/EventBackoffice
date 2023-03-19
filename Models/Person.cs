using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBackofficeBackend.Models;
public class Person
{
    // Primary Key
    public int PersonID {get; set;}
    
    // Properties
    public string Name {get; set;}
    [Url]
    public string Picture {get; set;}
    public string Bio {get; set;}
    [EmailAddress]
    public string Email {get; set;}

    // Relationships

    // One-to-many
    public ICollection<Paper> Papers;
    public ICollection<Paper> Presenting;
}