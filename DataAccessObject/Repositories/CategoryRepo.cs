using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class CategoryRepo : Repository<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext db) : base(db)
        {
            
        }
    }
}
