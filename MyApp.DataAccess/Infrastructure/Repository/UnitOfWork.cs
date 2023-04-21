using MyApp.DataAccess.Infrastructure.IRepository;
using MyWebApplication.Data;

namespace MyApp.DataAccess.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepositroy(context);
            Product = new ProductRepository(context);

        }
        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
