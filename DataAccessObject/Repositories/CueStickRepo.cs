using DataAccessObject.Data;
using DataAccessObject.Models;
using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.Repositories
{
    public class CueStickRepo : Repository<CueStick>, ICueStickRepo
    {
        public CueStickRepo(ApplicationDbContext db) : base(db)
        {

        }
    }
}
