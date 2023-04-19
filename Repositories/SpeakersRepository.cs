using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackoffice.Backend.Data;
using EventBackoffice.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using EventBackoffice.Backend.Models.DTOs.Speaker;

namespace EventBackoffice.Backend.Repositories;
public class SpeakersRepository
{
    private readonly BackendContext _context;

    public SpeakersRepository(BackendContext context) 
    {
        _context = context;
    }

    public async Task<Speaker> CreateAsync(PostSpeakerRequest request) 
    {
        if (request.EventID is int eventID && eventID != 0) 
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventID == eventID);
            if (@event is not null)
            {
                var speaker = new Speaker {Event = @event, Name = request.Name};
                await _context.AddAsync(speaker);
                await _context.SaveChangesAsync();

                return speaker;
            }
            else throw new KeyNotFoundException("An event with the given ID was not found");
        }
        else throw new ArgumentException("The Event ID is not valid");
    }

        public async Task<bool> DeleteAsync(int id)
    {
        var speaker = await _context.Speakers.FirstOrDefaultAsync(s => s.PersonID == id);

        if (speaker is null)
        {
            throw new KeyNotFoundException();
        }

        _context.Speakers.Remove(speaker);
        await _context.SaveChangesAsync();

        return true;
    }

    // public async Task<ActionResult> GetSpeakersAsync()
    // {
    //     var queryable = _context.Speakers.AsQueryable();

    //     return await queryable.ToListAsync();
    // }

    public async Task<Speaker> GetSpeakerByIdAsync(int id)
    {
        var speakers = _context.Speakers;

        if (speakers is not null)
        {
            var speaker = await speakers.FirstOrDefaultAsync(s => s.PersonID == id);
            return speaker ?? throw new KeyNotFoundException("No speaker found with ID " + id); 
        }
        else
        {
            throw new Exception("DBSet Speakers is null");
        } 
    }

 
}