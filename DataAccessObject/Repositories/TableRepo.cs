using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class TableRepo : Repository<Table>, ITableRepo
    {
        public TableRepo(ApplicationDbContext db) : base(db)
        {
        }
    }
}
