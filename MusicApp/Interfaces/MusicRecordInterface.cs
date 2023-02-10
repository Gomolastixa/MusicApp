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

    
           
        
    }
}
