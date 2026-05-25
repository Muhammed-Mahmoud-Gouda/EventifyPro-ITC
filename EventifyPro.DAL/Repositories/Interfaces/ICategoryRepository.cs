namespace EventifyPro.DAL.Repositories.Interfaces;

public interface ICategoryRepository : IGenericRepository<Category>
{
    /// <summary>
    /// Checks if a category with the given name already exists — case-insensitive.
    /// Used before Create/Update to enforce uniqueness at service level.
    /// </summary>
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a category is referenced by at least one event.
    /// Used before Delete to prevent breaking the FK Restrict constraint.
    /// </summary>
    Task<bool> IsUsedByAnyEventAsync(int categoryId, CancellationToken cancellationToken = default);

    /// <summary>Returns all categories ordered by name — for dropdowns.</summary>
    Task<IReadOnlyList<Category>> GetAllOrderedAsync(CancellationToken cancellationToken = default);
}