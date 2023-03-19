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
    public class RoomsRepository
    {
        private readonly EventBackofficeBackendContext _context;

        public RoomsRepository(EventBackofficeBackendContext context) 
        {
            _context = context;
        }

        public async Task CreateAsync(Room room) 
        {
            if (room.RoomID is not 0) 
            {
                throw new InvalidOperationException();
            }

            await _context.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsAsync(bool asNoTracking = false)
        {
            var queryable = _context.Rooms.AsQueryable();
            
            if (asNoTracking)
            {
                return await queryable.AsNoTracking().ToListAsync();
            }
            
            return await queryable.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id, bool asNoTracking = false)
        {
            var queryable = _context.Rooms.AsQueryable();

            if (asNoTracking)
            {
                return await queryable.AsNoTracking().FirstOrDefaultAsync(s => s.RoomID == id);
            }

            return await queryable.FirstOrDefaultAsync(s => s.RoomID == id); 
        }

        public async Task DeleteAsync(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(s => s.RoomID == id);

            if (room is null)
            {
                throw new InvalidOperationException();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        } 
    }