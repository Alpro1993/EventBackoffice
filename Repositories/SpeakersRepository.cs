using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Repositories;
public class SpeakersRepository
{
    private readonly EventBackofficeBackendContext _context;

    public SpeakersRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Speaker speaker) 
    {
        if (speaker.PersonID != 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(speaker);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Speaker>> GetSpeakersAsync(bool asNoTracking = false)
    {
        var queryable = _context.Speakers.AsQueryable();
        
        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }
        
        return await queryable.ToListAsync();
    }

    public async Task<Speaker> GetSpeakerByIdAsync(int id)
    {
        var speakers = _context.Speakers;

        if (speakers is not null)
        {
            var speaker = await speakers.FirstOrDefaultAsync(s => s.PersonID == id);
            return speaker ?? throw new ArgumentException("No speaker found with ID " + id); 
        }
        else
        {
            throw new Exception("DBSet Speakers is null");
        } 
    }

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