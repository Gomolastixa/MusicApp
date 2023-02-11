using MusicApp.Models;

namespace MusicApp.Services
{
    public interface IMusicRecordInterface : IGenericInterface<MusicRecord>
    {
        Task<MusicRecord> GetByIdAsync(int? id);
    }
}
