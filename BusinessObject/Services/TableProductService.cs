using BusinessObject.Services.Interface;
using DataAccessObject.Models;
using DataAccessObject.UnitOfWork;

namespace BusinessObject.Services
{
    public class TableProductService : ITableProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TableProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProductAsync(Guid tableSessionId, Guid productId, int quantity)
        {
            var tableSession = await _unitOfWork.TableSessionRepo.GetAsync(
                ts => ts.TableSessionID == tableSessionId,
                includeProperties:"TableProducts,TableProducts.Product", asNoTracking:false
            );

            if (tableSession == null)
                throw new Exception("Table session không tồn tại.");

            var product = await _unitOfWork.ProductRepo
                .GetAsync(p => p.ProductID == productId);

            if (product == null)
                throw new Exception("Sản phẩm không tồn tại.");

            var existingTableProduct = tableSession.TableProducts!
                .FirstOrDefault(tp => tp.ProductID == productId);

            if(existingTableProduct != null)
            {
                existingTableProduct.Quantity += quantity;
                existingTableProduct.SubTotal = existingTableProduct.Quantity * product.Price;
            }
            else
            {
                var tableProduct = new TableProduct
                {
                    TableProductID = Guid.NewGuid(),
                    TableSessionID = tableSessionId,
                    ProductID = productId,
                    Quantity = quantity,
                    SubTotal = product.Price * quantity
                };
                if (tableSession.TableProducts == null)
                    tableSession.TableProducts = new List<TableProduct>();

                tableSession.TableProducts.Add(tableProduct);

                await _unitOfWork.TableProductRepo.AddAsync(tableProduct);
            }     
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid tableProductId, Guid tableSessionId)
        {
            var tableProduct = await _unitOfWork.TableProductRepo
                .GetAsync(tp => tp.TableProductID == tableProductId && tp.TableSessionID == tableSessionId);
            if(tableProduct != null)
            {
                _unitOfWork.TableProductRepo.Remove(tableProduct);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
