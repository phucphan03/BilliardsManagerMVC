using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class TableProductRepo : Repository<TableProduct>, ITableProductRepo
    {
        public TableProductRepo(ApplicationDbContext db) : base(db)
        {
        }
    }
}
