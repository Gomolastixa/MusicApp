namespace MusicApp.Services
{
    public interface IGenericInterface<T> where T : class
    {

        Task<T> AddAsync(T entity);
        Task<T> CreateRecordMemberASync(int id,T entity);

    }
}
