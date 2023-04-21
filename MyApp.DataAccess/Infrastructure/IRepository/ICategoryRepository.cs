using MyApp.Models;

namespace MyApp.DataAccess.Infrastructure.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
    }
}
