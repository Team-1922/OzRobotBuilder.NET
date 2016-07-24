using System.Threading.Tasks;

namespace Team1922.Contracts
{
    public interface IUpdateRepository<T, in TKey>
        where T : class
    {
        Task<T> AddAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<bool> DeleteAsync(TKey id);
    }
}
