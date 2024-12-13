using Microsoft.EntityFrameworkCore;
using NotifyMaster.Core.Interfaces;
using NotifyMaster.Core.Specification;
using NotifyMaster.Infrastructure.Data;

namespace NotifyMaster.Infrastructure.Repositories;

public class BaseRelationalRepository<T> : IRelationalRepository<T> where T : class
{
    protected readonly NotifyMasterDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRelationalRepository(NotifyMasterDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            _context.RemoveRange(entities);
            return SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not remove entity {typeof(T)}", ex);
        }
    }

    public async Task<ICollection<T>> FindBySpecificationAsync(ISpecification<T> specification)
    {
        try
        {
            var query = _dbSet.AsQueryable();

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }

            foreach (var includeString in specification.IncludeStrings)
            {
                query = query.Include(includeString);
            }

            if (specification.AsNotTracking)
            {
                query = query.AsNoTracking();
            }

            if (specification.AsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            var entities = await query.ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not get entities {typeof(T)} by Specification", ex);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
