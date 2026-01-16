using BusinessObject.Services.Interface;
namespace BusinessObject.FacadeService
{
    public interface IFacadeService
    {
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
        ICueStickService CueStickService { get; }
        ITableService TableService { get; }
        ITableSessionService TableSessionService { get; }
        ITableProductService TableProductService { get; }
        ITableSessionCueService TableSessionCueService { get; }
    }
}
