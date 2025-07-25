using Microsoft.EntityFrameworkCore;
using LoanManager.Domain.Abstractions;
using LoanManager.Infrastructure.Specifications;
namespace LoanManager.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId>
where TEntity : Entity<TEntityId>
where TEntityId: class
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task <TEntity?> GetByIdAsync(
        TEntityId id,
        CancellationToken cancellationToken = default
    )
    {
        return await DbContext.Set<TEntity>()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }


    public void Delete(TEntity entity){
        DbContext.Remove(entity);
    }

    public virtual void Add(TEntity entity)
    {
        DbContext.Add(entity);
    }

    public IQueryable<TEntity> ApplySpecification(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return SpecificationsEvaluator<TEntity, TEntityId>
        .GetQuery(DbContext.Set<TEntity>().AsQueryable(), spec);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync(
        ISpecification<TEntity, TEntityId> spec
    )
    {
        return await ApplySpecification(spec).CountAsync();
    } 

}