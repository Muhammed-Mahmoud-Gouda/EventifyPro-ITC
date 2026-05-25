namespace EventifyPro.DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(EventifyDbContext context) : base(context) { }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
        => await _dbSet
            .AnyAsync(c => c.Name.ToLower() == name.ToLower(), cancellationToken);
  
    public async Task<bool> IsUsedByAnyEventAsync(int categoryId, CancellationToken cancellationToken = default)
        => await _context.Set<Event>()
            .AnyAsync(e => e.CategoryId == categoryId, cancellationToken);

    public async Task<IReadOnlyList<Category>> GetAllOrderedAsync(CancellationToken cancellationToken = default)
        => await _dbSet
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
}