using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicApp.Data;
using MusicApp.Models;
using MusicApp.Services;

namespace MusicApp.Interfaces
{
    public class MusicRecordInterface : IMusicRecordInterface
    {
        private readonly ApplicationDbContext _context;

        public MusicRecordInterface(ApplicationDbContext context)
        {
            _context = context;

        }

        public async void AddMemberAsync(int id, Musician musician)
        {
            var recordMember = new RecordMember { MusicRecordId = id,MusicianId= musician.Id };

            _context.Add(recordMember);
            await _context.SaveChangesAsync();
           
        }
    }
}
