using AutoMapper;
using Contracts.Domains.Interfaces;
using Infrastructure.Common;
using Infrastructure.Common.Models;
using Infrastructure.Extensions;
using Product.API.Repositories.Interfaces;
using Product.Infrastructure.Persistence;
using Shared.DTOs.Product;

namespace Product.API.Repository;

public class ProductCategoryRepository : RepositoryBase<Domain.Entities.ProductCategory, long, ProductContext>, IProductCategoryRepository
{
    private readonly ProductContext _dbContext;
    private readonly IMapper _mapper;

    public ProductCategoryRepository(ProductContext dbContext, IMapper mapper, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext, unitOfWork)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<ProductCategoryDto>> GetProductCategoriesPagingAsync(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10)
    {
        var query = _dbContext.ProductCategories.AsQueryable();

        // FIND
        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(x => x.Name.Contains(keyword));
        }

        // MAPPING
        var mappingResult = _mapper.ProjectTo<ProductCategoryDto>(query);

        // PAGING
        var pagedResult = await mappingResult.PaginatedListAsync(sortField, sortOrder,  pageIndex, pageSize);

        // RESPONSE
        return pagedResult;
    }
}