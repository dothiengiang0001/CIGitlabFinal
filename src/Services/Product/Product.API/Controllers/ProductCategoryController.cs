using AutoMapper;
using Infrastructure.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto;
using Product.API.Repositories.Interfaces;
using Product.API.Repository;
using Shared.DTOs.Product;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;


    public ProductCategoryController(IProductCategoryRepository ProductCategoryRepository, IProductRepository productRepository, IMapper mapper)
    {
        _productCategoryRepository = ProductCategoryRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    #region CRUD

    [HttpPost]
    public async Task<IActionResult> CreateProductCategory([FromBody] CreateUpdateProductCategoryDto ProductCategoryDto)
    {
        // CHECKING ProductCategory Name is exited ?
        var productCategoryEntity = await _productCategoryRepository.FindByCondition(x => x.Name.ToLower().Trim() == ProductCategoryDto.Name.ToLower().Trim()).ToListAsync();
        if (productCategoryEntity != null) return BadRequest($"ProductCategory Name: {ProductCategoryDto.Name} is existed.");

        var productCategory = _mapper.Map<Domain.Entities.ProductCategory>(ProductCategoryDto);
        await _productCategoryRepository.CreateAsync(productCategory);
        var result = _mapper.Map<ProductCategoryDto>(productCategory);
        return Ok(result);
    }

    [HttpGet("paging")]
    public async Task<ActionResult<PagedList<ProductCategoryDto>>> GetProductCategoryPaging(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var pagedList = await _productCategoryRepository.GetProductCategoriesPagingAsync(sortField, sortOrder, keyword, pageIndex, pageSize);
            if (pagedList == null)
            {
                var responseApiError = new ApiErrorResult<PagedList<ProductCategoryDto>>("Error Ocurs !");
                return NotFound(responseApiError);
            }

            var responseApiSuccess = new ApiSuccessResult<PagedList<ProductCategoryDto>>(pagedList, "get ProductCategory paging is successful !");
            return Ok(responseApiSuccess);
        }
        catch (Exception)
        {
            var responseApiError = new ApiErrorResult<PagedList<ProductCategoryDto>>("Error Ocurs !");
            return NotFound(responseApiError);
        }
    }


    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductCategoryDto>> GetProductCategory([Required] long id)
    {
        try
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound(new ApiErrorResult<ProductCategoryDto>("get ProductCategory is failed !"));
            }

            var result = _mapper.Map<ProductCategoryDto>(productCategory);
            return Ok(new ApiSuccessResult<ProductCategoryDto>(result, "get ProductCategory is successful !"));
        }
        catch (Exception)
        {
            return BadRequest(new ApiErrorResult<ProductCategoryDto>("Error Ocurs !"));
        }
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductCategoryDto>> UpdateProductCategory([Required] long id, [FromBody] CreateUpdateProductCategoryDto productCategoryDto)
    {
        try
        {
            // CHECKING
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound(new ApiErrorResult<PagedList<ProductCategoryDto>>("Error Occurs !"));
            }

            // MAPPING AND UPDATE
            var updateProductCategory = _mapper.Map(productCategoryDto, productCategory);
            await _productCategoryRepository.UpdateAsync(updateProductCategory);
            var result = _mapper.Map<ProductCategoryDto>(productCategory);

            // RETURN 
            return Ok(new ApiSuccessResult<ProductCategoryDto>(result, "Update ProductCategory is successful !"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiErrorResult<ProductCategoryDto>("Error Ocurs !"));
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ProductCategoryDto>> DeleteProductCategory(long id)
    {
        try
        {
            // CHECKING AND DELETE
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound(new ApiErrorResult<ProductCategoryDto>("Not found product category !"));
            }
            var isExistedCatePro = _productRepository.FindByCondition(x => x.CategoryID == id) != null;
            if (isExistedCatePro)
            {
                return BadRequest(new ApiErrorResult<ProductCategoryDto>("To delete category, you have to delete all product that's this category !"));
            }
            await _productCategoryRepository.DeleteAsync(productCategory);

            // MAPPING AND RETURN
            var result = _mapper.Map<ProductCategoryDto>(productCategory);
            return Ok(new ApiSuccessResult<ProductCategoryDto>(result, "Delete ProductCategory is successful !"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiErrorResult<ProductCategoryDto>("Error Ocurs !"));
        }
    }

    [HttpDelete]
    public async Task<ActionResult<long[]>> DeleteMultiProductCategory(long[] ids)
    {
        // CHECKING
        var isExistedCatePro = _productRepository.FindByCondition(x => ids.Contains(x.CategoryID)) != null;
        if (isExistedCatePro)
        {
            return BadRequest(new ApiErrorResult<long[]> ("To delete category, you have to delete all product that's this category !"));
        }

        // Bắt đầu một transaction
        using (var transaction = await _productCategoryRepository.BeginTransactionAsync())
        {
            try
            {
                var ProductCategory = _productCategoryRepository.FindByCondition(x => ids.Contains(x.Id));
                // Thực hiện xóa danh sách sản phẩm
                _productCategoryRepository.DeleteList(ProductCategory);

                // Lưu thay đổi và commit transaction
                await _productCategoryRepository.EndTransactionAsync();
                return Ok(new ApiSuccessResult<long[]>(ids, "Delete ProductCategory is successful !"));
            }
            catch (Exception)
            {
                // Nếu có lỗi, rollback transaction
                await _productCategoryRepository.RollbackTransactionAsync();
                return BadRequest(new ApiErrorResult<long[]>("Error Ocurs !"));
            }
        }
    }

    #endregion

}