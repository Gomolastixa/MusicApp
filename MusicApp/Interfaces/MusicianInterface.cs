using Microsoft.EntityFrameworkCore;
using MusicApp.Data;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Interfaces
{
    public class MusicianInterface : GenericInterface<Musician>, IMusicianInterface
    {
        private readonly ApplicationDbContext _context;

        public MusicianInterface(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var musician = await GetByIdAsync(id);
            _context.Musicians.Remove(musician);
            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> Exists(int id)
        {
            var e = await GetByIdAsync(id);

            return e != null;
        }

        public async Task<Musician> GetByIdAsync(int? id)
        {
            return await _context.Musicians.Include(rm => rm.RecordMembers)
                                           .ThenInclude(mr => mr.MusicRecord)
                                           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Musician> GetByNameAsync(string name)
        {
            return await _context.Musicians.Include(rm => rm.RecordMembers)
                                           .ThenInclude(mr => mr.MusicRecord)
                                           .FirstOrDefaultAsync(m => m.FullName == name);
        }
    }
}
