namespace InvoiceAppDomain.Data.Repository
{
    public interface IRepository<TEntity, TIdType>
        where TEntity : class
        where TIdType : struct
    {
        Task<IList<TEntity>> GetAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> filter,
            CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(TIdType id, CancellationToken cancellationToken = default);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(TIdType id, CancellationToken cancellationToken = default);
    }
}