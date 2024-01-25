using Core.Persistence.Paging;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public abstract class EfBaseTimeStampRepositoryBase<TEntity, TContext> : EfRepositoryBase<TEntity, TContext>, IAsyncBaseTimeStampRepository<TEntity>, IBaseTimeStampRepository<TEntity>
        where TEntity : BaseTimeStampEntity
        where TContext : DbContext
{
    public EfBaseTimeStampRepositoryBase(TContext context) : base(context)
    {
    }

    public IPaginate<TEntity> GetListDeleted(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        queryable.Where(x => x.IsDeleted == true);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public async Task<IPaginate<TEntity>> GetListDeletedAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        queryable.Where(x => x.IsDeleted == true);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }

    public IPaginate<TEntity> GetListNotDeleted(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true)
    {
        IQueryable<TEntity> queryable = Query();
        queryable.Where(x => x.IsDeleted != true);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return orderBy(queryable).ToPaginate(index, size);
        return queryable.ToPaginate(index, size);
    }

    public async Task<IPaginate<TEntity>> GetListNotDeletedAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        queryable.Where(x => x.IsDeleted != true);
        if (!enableTracking) queryable = queryable.AsNoTracking();
        if (include != null) queryable = include(queryable);
        if (predicate != null) queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
    }
    public override async Task<TEntity> AddAsync(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.CreatedAt = DateTime.Now;
        entity.CreatedUser = info?.FullName ?? "Unknown";
        entity.DeletedUserId = info?.Id ?? Guid.Empty;
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }
    public override TEntity Add(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.CreatedAt = DateTime.Now;
        entity.CreatedUser = info?.FullName ?? "Unknown";
        entity.DeletedUserId = info?.Id ?? Guid.Empty;
        Context.Entry(entity).State = EntityState.Added;
        Context.SaveChanges();
        return entity;
    }
    public override async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.CreatedAt = DateTime.Now;
        entity.CreatedUser = info?.FullName ?? "Unknown";
        entity.DeletedUserId = info?.Id ?? Guid.Empty;
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }
    public override TEntity Update(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.UpdatedAt = DateTime.Now;
        entity.UpdatedUser = info?.FullName ?? "Unknown";
        entity.UpdatedUserId = info?.Id ?? Guid.Empty;
        entity.IsUpdated = true;
        Context.Entry(entity).State = EntityState.Added;
        Context.SaveChanges();
        return entity;
    }


    public TEntity Restore(TEntity entity)
    {
        entity.DeletedAt = null;
        entity.DeletedUser = null;
        entity.DeletedUserId = null;
        entity.IsDeleted = false;
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> RestoreAsync(TEntity entity)
    {
        entity.DeletedAt = null;
        entity.DeletedUser = null;
        entity.DeletedUserId = null;
        entity.IsDeleted = false;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }

    public TEntity SoftDelete(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.DeletedAt = DateTime.Now;
        entity.DeletedUser = info?.FullName ?? "Unknown";
        entity.DeletedUserId = info?.Id ?? Guid.Empty;
        entity.IsDeleted = true;
        Context.Entry(entity).State = EntityState.Modified;
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> SoftDeleteAsync(TEntity entity)
    {
        var info = ServiceTool.GetUserInfo();
        entity.DeletedAt = DateTime.Now;
        entity.DeletedUser = info?.FullName ?? "Unknown";
        entity.DeletedUserId = info?.Id ?? Guid.Empty;
        entity.IsDeleted = true;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }
}