using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using MyWebApplication.Data;

namespace MyApp.DataAccess.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productsdb = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (productsdb != null)
            {
                productsdb.Name = product.Name;
                productsdb.Description = product.Description;
                productsdb.Price = product.Price;
                if (product.ImageUrl != null)
                {
                    productsdb.ImageUrl = product.ImageUrl;
                }

            }
        }
    }
}
