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
public class SponsorsRepository
{
    private readonly EventBackofficeBackendContext _context;

    public SponsorsRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Sponsor sponsor) 
    {
        if (sponsor.SponsorID is not 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(sponsor);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Sponsor>> GetSponsorsAsync(bool asNoTracking = false)
    {
        var queryable = _context.Sponsors.AsQueryable();
        
        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }
        
        return await queryable.ToListAsync();
    }

    public async Task<Sponsor> GetSponsorByIdAsync(int id, bool asNoTracking = false)
    {
        var queryable = _context.Sponsors.AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.SponsorID == id);
        }

        return await queryable.FirstOrDefaultAsync(s => s.SponsorID == id); 
    }

    public async Task DeleteAsync(int id)
    {
        var sponsor = await _context.Sponsors.FirstOrDefaultAsync(s => s.SponsorID == id);

        if (sponsor is null)
        {
            throw new InvalidOperationException();
        }

        _context.Sponsors.Remove(sponsor);
        await _context.SaveChangesAsync();
    } 
}