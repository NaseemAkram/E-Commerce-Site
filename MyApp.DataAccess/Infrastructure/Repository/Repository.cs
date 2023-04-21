using Microsoft.EntityFrameworkCore;
using MyApp.DataAccess.Infrastructure.IRepository;
using MyWebApplication.Data;
using System.Linq.Expressions;

namespace MyApp.DataAccess.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbset;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
            //_context.Products.Include(x => x.Category);
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbset.RemoveRange(entity);
        }

        public IEnumerable<T> GetAll(string? incudeProperties = null)
        {
            IQueryable<T> query = _dbset;
            if (incudeProperties != null)
            {
                foreach (var item in incudeProperties.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return _dbset.ToList();
        }

        public T GetT(Expression<Func<T, bool>> predicate, string? incudeProperties = null)
        {

            IQueryable<T> query = _dbset;
            query = query.Where(predicate);
            if (incudeProperties != null)
            {
                foreach (var item in incudeProperties.Split(new char[] { ',', }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }
    }
}
