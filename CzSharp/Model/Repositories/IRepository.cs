using System.Linq;
using System.Threading.Tasks;

namespace CzSharp.Model.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Returns all elements in table
        /// </summary>
        /// <returns></returns>
        IQueryable<T> FindAll();
        
        /// <summary>
        /// Return element specified by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindByIdAsync(int id);
        
        /// <summary>
        /// Creates element in table
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task CreateAsync(T item);
        
        /// <summary>
        /// Updates element in table
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(T item);
        
        /// <summary>
        /// Deletes element in table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
        
        /// <summary>
        /// Deletes element in table by element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteAsync(T item);
    }
}