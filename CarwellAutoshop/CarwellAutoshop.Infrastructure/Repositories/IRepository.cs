using System.Linq.Expressions;

namespace CarwellAutoshop.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task<T?> GetByIdAsync(
        Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes
    );

        Task<List<T>> GetAllAsync(
            params Expression<Func<T, object>>[] includes
        );
    }

}
