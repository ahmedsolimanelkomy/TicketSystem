using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string[]? includeProperties = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter, string[]? includeProperties = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
