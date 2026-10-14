using EF_Task.DTO;
using EF_Task.Enum;

namespace EF_Task.Services
{
    public interface IProductService
    {
        Task<object> GetProductsAsync(int pageNumber, int pageSize);

        Task<bool> SoftDeleteProductAsync(int id);


        Task<UpdateProductResult> UpdateProductAsync(int id, UpdateProductDto dto);

    }
}
