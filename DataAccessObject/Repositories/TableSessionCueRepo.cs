using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class TableSessionCueRepo : Repository<TableSessionCue>, ITableSessionCueRepo
    {
        public TableSessionCueRepo(ApplicationDbContext db) : base(db)
        {
        }
    }
}
