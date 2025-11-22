using DataAccessObject.Data;
using DataAccessObject.Repositories.Interface;
using Microsoft.Extensions.Options;
using Ultitity.Options;

namespace DataAccessObject.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public ICategoryRepo CategoryRepo { get; private set; }
        public IProductRepo ProductRepo { get; private set; }
        public ICueStickRepo CueStickRepo { get; private set; }
        public ITableRepo TableRepo { get; private set; }
        public ITableProductRepo TableProductRepo { get; private set; }
        public ITableSessionRepo TableSessionRepo { get; private set; }
        public ITableSessionCueRepo TableSessionCueRepo { get; private set; }
        public IImageRepo ImageRepo { get; private set; }

        public UnitOfWork(ApplicationDbContext db, IOptions<CloudinaryOptions> cloudaryOptions)
        {
            _db = db;
            CategoryRepo = new Repositories.CategoryRepo(_db);
            ProductRepo = new Repositories.ProductRepo(_db);
            CueStickRepo = new Repositories.CueStickRepo(_db);
            TableRepo = new Repositories.TableRepo(_db);
            TableProductRepo = new Repositories.TableProductRepo(_db);
            TableSessionRepo = new Repositories.TableSessionRepo(_db);
            TableSessionCueRepo = new Repositories.TableSessionCueRepo(_db);
            ImageRepo = new Repositories.ImageRepo(_db, cloudaryOptions);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
