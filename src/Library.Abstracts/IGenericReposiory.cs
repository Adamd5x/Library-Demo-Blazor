#nullable enable

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Library.Abstracts;

public interface IGenericReposiory<TEntity> where TEntity : class
{
    Task<TEntity> InsertAsync (TEntity entity);

    void Update (TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync (Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null, int offset = 0, int size = 10);

    Task<TEntity?> GetAsync (Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null);

    Task DeleteAsync (int id);
}
