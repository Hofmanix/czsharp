using System.Linq;
using System.Threading.Tasks;

namespace CzSharp.Model.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        Task<T> FindByIdAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task DeleteAsync(T item);
    }
}