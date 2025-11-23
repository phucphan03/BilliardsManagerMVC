namespace BusinessObject.Services.Interface
{
    public interface ITableProductService
    {
        Task AddProductAsync(Guid tableSessionId, Guid productId, int quantity);
        Task DeleteProductAsync(Guid tableProductId, Guid tableSessionId);
    }
}
