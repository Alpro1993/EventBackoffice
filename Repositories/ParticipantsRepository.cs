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
    public class ParticipantsRepository
    {
        private readonly EventBackofficeBackendContext _context;

        public ParticipantsRepository(EventBackofficeBackendContext context) 
        {
            _context = context;
        }

        public async Task CreateAsync(Participant participant) 
        {
            if (participant.PersonID is not 0) 
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
            var queryable = _context.Participants.AsQueryable();

            if (asNoTracking)
            {
                return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.PersonID == id);
            }

            return await queryable.FirstOrDefaultAsync(s => s.PersonID == id); 
        }

        public async Task DeleteAsync(int id)
        {
            var participant = await _context.Participants.FirstOrDefaultAsync(s => s.PersonID == id);

            if (participant is null)
            {
                throw new InvalidOperationException();
            }

            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
        } 
    }