using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackoffice.Backend.Data;
using EventBackoffice.Backend.Models;

namespace EventBackoffice.Backend.Repositories;
public class VenuesRepository
{
    private readonly BackendContext _context;

    public VenuesRepository(BackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Venue venue) 
    {
        if (venue.VenueID != 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(venue);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Venue>> GetVenuesAsync(bool asNoTracking = false)
    {
        var queryable = _context.Venues.AsQueryable();
        
        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }
        
        return await queryable.ToListAsync();
    }

    public async Task<Venue> GetVenueByIdAsync(int id, bool asNoTracking = false)
    {
        var venues = _context.Venues;

        if (venues is not null)
        {
            var venue = await venues.FirstOrDefaultAsync(s => s.VenueID == id);
            return venue ?? throw new ArgumentException("No venue found with ID " + id);  
        }
        else throw new Exception("DBSet Venues is null");
    }

    public async Task DeleteAsync(int id)
    {
        var venue = await _context.Venues.FirstOrDefaultAsync(s => s.VenueID == id);

        if (venue is null)
        {
            throw new InvalidOperationException();
        }

        _context.Venues.Remove(venue);
        await _context.SaveChangesAsync();
    } 
}