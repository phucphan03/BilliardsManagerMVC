using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class TableSessionRepo : Repository<TableSession>, ITableSessionRepo
    {
        public TableSessionRepo(ApplicationDbContext db) : base(db)
        {
        }
    }
}
