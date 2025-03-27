namespace EcommerceNet8.Core.Aplication.Persistence
{
    public interface IUnitOfWork :IDisposable
    {
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class;

    }
}
