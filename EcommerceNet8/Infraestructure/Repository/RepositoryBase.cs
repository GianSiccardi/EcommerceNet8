using EcommerceNet8.Core.Aplication.Persistence;
using EcommerceNet8.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using EcommerceNet8.Core.Aplication.Specifications;
using EcommerceNet8.Infraestructure.Specifications;

namespace EcommerceNet8.Infraestructure.Repository
{
    public class RepositoryBase<T>  :IAsyncRepository<T>where T :class
    {
        protected readonly EcommerceDbContext _context;

        public RepositoryBase(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //este solo inserta en memoria
        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);

        }

        //agrega en nivel de memoria
        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

      

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IReadOnlyList<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>(); //Inicia la consulta 
            if (disableTracking) query = query.AsNoTracking();// deshabilita seguimiento de cambios , para mejorar el rendmiento

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString); // carga relaciones

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await _context.Set<T>().FindAsync(id));
        }

    


        // devuelve primera entidad q coincida con el filtro 
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);
            return (await query.FirstOrDefaultAsync());
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return (await ApplySpecification(spec).FirstOrDefaultAsync())!;
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecifitationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

    }
}
