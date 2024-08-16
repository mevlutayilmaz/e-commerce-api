using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>>? method = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false);
        IQueryable<T> GetAllByPaging(Expression<Func<T, bool>>? method = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int pageCount = 0, 
            int itemCount = 5, 
            bool enableTracking = false);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool enableTracking = false);
        Task<T> GetByIdAsync(string id, bool enableTracking = false);
        Task<int> CountAsync(Expression<Func<T, bool>>? method = null);

    }
}
