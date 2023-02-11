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


    }
}
