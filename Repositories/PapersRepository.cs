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

public class PapersRepository
{
    public required BackendContext _context;

    public PapersRepository(BackendContext context) 
    {
        _context = context;
    }

    public async Task CreateAsync(Paper paper) 
    {
        if (paper.PaperID != 0) 
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

    public async Task<Paper> GetPaperByIdAsync(int id)
    {  
            return await _context.Papers.FirstOrDefaultAsync(s => s.PaperID == id) 
                    ?? throw new ArgumentException("No paper found with ID " + id); 
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
