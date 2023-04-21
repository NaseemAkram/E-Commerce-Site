using MyApp.Models;

namespace MyApp.DataAccess.Infrastructure.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
