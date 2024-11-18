#nullable enable

using System.Linq.Expressions;
using Library.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Library.Repository;

public class GenericRepository<TEntity> (LibraryDbContext dbContext) : IGenericReposiory<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> db = dbContext.Set<TEntity>();

    public async Task DeleteAsync (int id)
    {
        var entry = await db.FindAsync(id);
        if (entry is not null)
        {
            db.Remove(entry);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync (Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null, int offset = 0, int size = 10)
    {
        IQueryable<TEntity> query = db;

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (includes is not null)
        {
            query = includes (query);
        }

        if (orderBy is not null)
        {
            query = orderBy (query);
        }

        return await query.AsNoTracking ()
                          .Skip (offset)
                          .Take (size)
                          .ToListAsync ();
    }

    public async Task<TEntity?> GetAsync (Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includes = null)
    {
        IQueryable<TEntity> query = db;
        if (includes is not null)
        {
            query = includes(query);
        }

        return await query.AsNoTracking()
                          .FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity> InsertAsync (TEntity entity)
    {
        var result = await db.AddAsync(entity);
        return result.Entity;
    }

    public void Update (TEntity entity)
    {
        db.Attach (entity);
        db.Entry(entity).State = EntityState.Modified;
    }
}
