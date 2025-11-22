using DataAccessObject.Repositories.Interface;

namespace DataAccessObject.UnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepo CategoryRepo { get; }
        IProductRepo ProductRepo { get; }
        ICueStickRepo CueStickRepo { get; }
        ITableRepo TableRepo { get; }
        ITableProductRepo TableProductRepo { get; }
        ITableSessionRepo TableSessionRepo { get; }
        ITableSessionCueRepo TableSessionCueRepo { get; }
        Task SaveAsync();
    }
}
