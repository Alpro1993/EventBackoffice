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

public class PapersRepository
{
    private readonly EventBackofficeBackendContext _context;

    public PapersRepository(EventBackofficeBackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Paper paper) 
    {
        if (paper.PaperID is not 0) 
        {
            throw new InvalidOperationException();
        }

        await _context.AddAsync(paper);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Paper>> GetPapersAsync(bool asNoTracking = false)
    {
        var queryable = _context.Papers.AsQueryable();
        
        if (asNoTracking)
        {
            return await queryable.AsNoTracking().ToListAsync();
        }
        
        return await queryable.ToListAsync();
    }

    public async Task<Paper> GetPaperByIdAsync(int id, bool asNoTracking = false)
    {
        var queryable = _context.Papers.AsQueryable();

        if (asNoTracking)
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.PaperID == id);
        }

        return await queryable.FirstOrDefaultAsync(s => s.PaperID == id); 
    }

    public async Task DeleteAsync(int id)
    {
        var paper = await _context.Papers.FirstOrDefaultAsync(s => s.PaperID == id);

        if (paper is null)
        {
            throw new InvalidOperationException();
        }

        _context.Papers.Remove(paper);
        await _context.SaveChangesAsync();
    } 
}
