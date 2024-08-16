using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ECommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
    {
        private readonly ECommerceDbContext _dbContext;
        public ReadRepository(ECommerceDbContext dbContext) => _dbContext = dbContext;
        public DbSet<T> Table => _dbContext.Set<T>();

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? method = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false)
        {
            var query = Table.AsQueryable();
            if (!enableTracking) query = query.AsNoTracking();
            if(include is not null) query = include(query);
            if(method is not null) query = query.Where(method);
            if(orderBy is not null) query = orderBy(query);
            return query;
        }
        public IQueryable<T> GetAllByPaging(Expression<Func<T, bool>>? method = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int pageCount = 1, 
            int itemCount = 5, 
            bool enableTracking = false)
        {
            var query = Table.AsQueryable();
            if (!enableTracking) query = query.AsNoTracking();
            if (include is not null) query = include(query);
            if (method is not null) query = query.Where(method);
            if (orderBy is not null) query = orderBy(query);
            return query.Skip(itemCount * (pageCount - 1)).Take(itemCount);
        }
        public async Task<T> GetByIdAsync(string id, bool enableTracking = false)
        {
            var query = Table.AsQueryable();
            if(!enableTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool enableTracking = false)
        {
            var query = Table.AsQueryable();
            if(!enableTracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? method = null)
        {
            Table.AsNoTracking();
            if (method is not null) return await Table.Where(method).CountAsync();
            return await Table.CountAsync();
        }
    }
}
