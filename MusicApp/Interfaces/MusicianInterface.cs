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
    }
}
