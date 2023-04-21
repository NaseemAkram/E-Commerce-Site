using MyApp.DataAccess.Infrastructure.IRepository;
using MyApp.Models;
using MyWebApplication.Data;

namespace MyApp.DataAccess.Infrastructure.Repository
{
    public class CategoryRepositroy : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        public CategoryRepositroy(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var categorydb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (categorydb != null)
            {
                categorydb.Name = category.Name;
                categorydb.DisplayOrder = category.DisplayOrder;
            }

        }
    }
}
