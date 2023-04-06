using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;
using Microsoft.AspNetCore.Mvc;
using EventBackofficeBackend.Models.DTOs.Speaker;

namespace EventBackofficeBackend.Repositories;
public class SpeakersRepository
{
    private readonly EventBackofficeBackendContext _context;

    public SpeakersRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    // public async Task<ActionResult> CreateAsync(PostSpeakerRequest request) 
    // {
    //     if (request.EventID is int eventID && eventID != 0) 
    //     {
    //         var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventID == eventID);
    //         if (@event is not null)
    //         {
    //             var speaker = new Speaker {Event = @event, Name = request.Name};
    //             await _context.AddAsync(speaker);
    //             await _context.SaveChangesAsync();

    //             return new ObjectCr
    //         }
    //         else return new BadRequestObjectResult("An Event with the given EventID was not found");
    //     }
    //     else return new BadRequestObjectResult("Given EventID is not valid.");
    // }

    // public async Task<ActionResult> GetSpeakersAsync()
    // {
    //     var queryable = _context.Speakers.AsQueryable();

    //     return await queryable.ToListAsync();
    // }

    // public async Task<ActionResult> GetSpeakerByIdAsync(int id)
    // {
    //     var speakers = _context.Speakers;

    //     if (speakers is not null)
    //     {
    //         var speaker = await speakers.FirstOrDefaultAsync(s => s.PersonID == id);
    //         return speaker ?? throw new ArgumentException("No speaker found with ID " + id); 
    //     }
    //     else
    //     {
    //         throw new Exception("DBSet Speakers is null");
    //     } 
    // }

    public async Task DeleteAsync(int id)
    {
        var speaker = await _context.Speakers.FirstOrDefaultAsync(s => s.PersonID == id);

        if (speaker is null)
        {
            throw new InvalidOperationException();
        }

        _context.Speakers.Remove(speaker);
        await _context.SaveChangesAsync();
    } 
}