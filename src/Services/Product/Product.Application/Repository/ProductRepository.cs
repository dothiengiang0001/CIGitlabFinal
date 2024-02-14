using AutoMapper;
using Contracts.Domains.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Product.API.Repositories.Interfaces;
using Product.Infrastructure.Persistence;
using Shared.DTOs.Product;
using Shared.SeedWork;

namespace Product.API.Repository;

public class ProductRepository : RepositoryBase<Domain.Entities.Product, long, ProductContext>, IProductRepository
{
    private readonly ProductContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(ProductContext dbContext, IMapper mapper, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Domain.Entities.Product>> GetPopularProductsAsync(int count)

    {
        return await _dbContext.Products.OrderByDescending(x => x.ViewCount).Take(count).ToListAsync();
    }

    public async Task<PagedList<ProductDto>> GetProductsPagingAsync(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10)
    {
        var query = _dbContext.Products.AsQueryable();

        // FIND
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Name.Contains(keyword));
        }

        // MAPPING
        var mappingResult = _mapper.ProjectTo<ProductDto>(query);

        // PAGING
        var pagedResult = await mappingResult.PaginatedListAsync(sortField, sortOrder, pageIndex, pageSize);

        // RESPONSE
        return pagedResult;
    }
}