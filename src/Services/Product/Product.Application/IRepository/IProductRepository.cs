using Contracts.Domains.Interfaces;
using Infrastructure.Common.Models;
using Product.Infrastructure.Persistence;
using Shared.DTOs.Product;

namespace Product.API.Repositories.Interfaces;

public interface IProductRepository : IRepositoryBase<Domain.Entities.Product, long, ProductContext>
{
    Task<IEnumerable<Product.Domain.Entities.Product>> GetPopularProductsAsync(int count);
    Task<PagedList<ProductDto>> GetProductsPagingAsync(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10);
}