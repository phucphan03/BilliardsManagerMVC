using BusinessObject.Services.Interface;
namespace BusinessObject.FacadeService
{
    public interface IFacadeService
    {
        ICategoryService CategoryService { get; }
        IProductService ProductService { get; }
    }
}
