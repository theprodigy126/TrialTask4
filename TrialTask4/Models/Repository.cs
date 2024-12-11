using Microsoft.EntityFrameworkCore;
using TrialTask4.Data;

namespace TrialTask4.Models;

public class Repository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _context { get; set; }
    private DbSet<T> _entities { get; set; }

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = _context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, QueryOptions<T> options)
    {
        IQueryable<T> query = _entities;
        
        if (options.HasWhere)
        {
            query = query.Where(options.Where);
        }

        if (options.HasOrderBy)
        {
            query = query.OrderBy(options.OrderBy);
        }

        foreach (string include in options.GetIncludes())
        {
            query = query.Include(include);
        }
        
        var key = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.FirstOrDefault();
        var propertyName = key?.Name;
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, propertyName) == id);
    }

    public async Task AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}