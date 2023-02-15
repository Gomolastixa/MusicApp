using MusicApp.Models;

namespace MusicApp.Services
{
    public interface IMusicRecordInterface : IGenericInterface<MusicRecord>
    {
        Task<MusicRecord> GetByIdAsync(int? id);
        Task<MusicRecord> GetByNameAsync(string name);
        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}
