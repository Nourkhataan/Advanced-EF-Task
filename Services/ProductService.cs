using EF_Task.Data;
using EF_Task.DTO;
using EF_Task.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EF_Task.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetProductsAsync(int pageNumber,int pageSize)
        {
            pageSize = Math.Min(pageSize, 100);

            var query = _context.Products
                .AsNoTracking()
                .Select(p => new ProductListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    RowVersion = p.RowVersion
                });

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pageSize),
                Products = products
            };
        }

        public async Task<bool> SoftDeleteProductAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        //public async Task<bool> UpdateProductAsync(
        //    int id,
        //    UpdateProductDto dto)
        //{
        //    var product = await _context.Products
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    if (product == null)
        //    {
        //        return false;
        //    }

        //    product.Name = dto.Name;
        //    product.Price = dto.Price;


        //    await _context.SaveChangesAsync();

        //    return true;
        //}

        public async Task<UpdateProductResult> UpdateProductAsync(int id,UpdateProductDto dto)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return UpdateProductResult.NotFound;
            }

            product.Name = dto.Name;
            product.Price = dto.Price;

            _context.Entry(product)
                .Property(p => p.RowVersion)
                .OriginalValue = dto.RowVersion;

            try
            {
                await _context.SaveChangesAsync();
                return UpdateProductResult.Success;
            }
            catch (DbUpdateConcurrencyException)
            {
                return UpdateProductResult.Conflict;
            }
        }



    }
}

