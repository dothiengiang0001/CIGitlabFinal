using Contracts.Domains.Interfaces;
using Infrastructure.Common.Models;
using Product.Infrastructure.Persistence;
using Shared.DTOs.Product;
using Shared.SeedWork;

namespace Product.API.Repositories.Interfaces;

public interface IProductCategoryRepository : IRepositoryBase<Domain.Entities.ProductCategory, long, ProductContext>
{
    Task<PagedList<ProductCategoryDto>> GetProductCategoriesPagingAsync(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10);
}