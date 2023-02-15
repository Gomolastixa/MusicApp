using MusicApp.Models;

namespace MusicApp.Services
{
    public interface IMusicianInterface : IGenericInterface<Musician>
    {
        Task<Musician> GetByIdAsync(int? id);
        Task<Musician> GetByNameAsync(string name);

        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}

