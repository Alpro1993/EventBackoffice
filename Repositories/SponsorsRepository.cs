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
public class SponsorsRepository
{
    private readonly BackendContext _context;

    public SponsorsRepository(BackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Sponsor sponsor) 
    {
        if (sponsor.SponsorID != 0) 
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

    public async Task<Sponsor> GetSponsorByIdAsync(int id)
    {
        var sponsors = _context.Sponsors;

        if (sponsors is not null)
        {
            var sponsor = await sponsors.FirstOrDefaultAsync(s => s.SponsorID == id); 
            return sponsor ?? throw new ArgumentException("No sponsor found with ID " + id);
        }
        else throw new Exception("DBSet Sponsors is null");
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