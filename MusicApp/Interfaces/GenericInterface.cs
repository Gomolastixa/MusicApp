using MusicApp.Data;
using MusicApp.Services;

namespace MusicApp.Interfaces
{
    public class GenericInterface<T> : IGenericInterface<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericInterface(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity;

        }

        public async Task<T> CreateRecordMemberASync(int id,T entity)
        {
            
            await _context.SaveChangesAsync();


            return entity;
        }
    }
}
