using MusicApp.Models;

namespace MusicApp.Services
{
    public interface IMusicRecordInterface
    {
      void AddMemberAsync(int id, Musician musician);
    }
}
