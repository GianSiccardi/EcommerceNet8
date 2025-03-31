using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Infraestructure.Persistence;
using System.Collections;

namespace EcommerceNet8.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private Hashtable? _repositories;

        private readonly EcommerceDbContext _context;


        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;

        }
        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error en transaccion", e);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories is null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>).MakeGenericType(typeof(TEntity)); // Construimos el tipo cerrado
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context); // Pasamos _context
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type]!;
        }
    }
}
