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
    public class ParticipantsRepository
    {
        public required EventBackofficeBackendContext _context;

        public ParticipantsRepository(EventBackofficeBackendContext context) 
        {
            _context = context;
        }

        public async Task CreateAsync(Participant participant) 
        {
            if (participant.PersonID != 0) 
            {
                throw new InvalidOperationException();
            }

            await _context.AddAsync(participant);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Participant>> GetParticipantsAsync(bool asNoTracking = false)
        {
            var queryable = _context.Participants.AsQueryable();
            
            if (asNoTracking)
            {
                return await queryable.AsNoTracking().ToListAsync();
            }
            
            return await queryable.ToListAsync();
        }

        public async Task<Participant> GetParticipantByIdAsync(int id, bool asNoTracking = false)
        { 
            return await _context.Participants.FirstOrDefaultAsync(s => s.PersonID == id) 
                    ?? throw new ArgumentException("No participant found with ID " + id);
        }

        public async Task DeleteAsync(int id)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(s => s.PersonID == id)
                                ?? throw new ArgumentException("No participant found with ID " + id);

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        } 
    }