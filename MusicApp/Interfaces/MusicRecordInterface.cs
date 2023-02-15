using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicApp.Data;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Interfaces
{
    public class MusicRecordInterface : GenericInterface<MusicRecord>, IMusicRecordInterface
    {
        private readonly ApplicationDbContext _context;

        public MusicRecordInterface(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<MusicRecord> GetByIdAsync(int? id)
        {

            return await _context.MusicRecord.Include(rm => rm.RecordMembers)
                                             .ThenInclude(m => m.Musician)
                                             .FirstOrDefaultAsync(mr => mr.Id == id);

        }

        public async Task<MusicRecord> GetByNameAsync(string name)
        {
            return await _context.MusicRecord.Include(rm => rm.RecordMembers)
                                             .ThenInclude(m => m.Musician)
                                             .FirstOrDefaultAsync(mr => mr.Name == name);
        }

        public async Task DeleteAsync(int id)
        {
            var musicRecord = await GetByIdAsync(id);
            _context.MusicRecord.Remove(musicRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var e = await GetByIdAsync(id);

            return e != null;
        }
    }
}
