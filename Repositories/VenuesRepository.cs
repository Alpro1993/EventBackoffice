using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using EventBackofficeBackend.Data;
using EventBackofficeBackend.Models;

namespace EventBackofficeBackend.Repository;
public class VenuesRepository
{
    private readonly EventBackofficeBackendContext _context;

    public VenuesRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Venue venue) 
    {
        if (venue.VenueID is not 0) 
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
        var queryable = _context.Venues.AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.VenueID == id);
        }

        return await queryable.FirstOrDefaultAsync(s => s.VenueID == id); 
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