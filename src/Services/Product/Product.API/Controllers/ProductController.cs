using AutoMapper;
using Infrastructure.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IMapper _mapper;
    
    public ProductController(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _mapper = mapper;
    }

    #region CRUD

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateUpdateProductDto productDto)
    {
        try
        {
            // CHECKING
            var productEntity = await _productRepository.FindByCondition(x => x.Name.ToLower().Trim() == productDto.Name.ToLower().Trim()).ToListAsync();
            if (productEntity != null)
            {
                return BadRequest(new ApiErrorResult<ProductDto>($"Product Name: {productDto.Name} is existed."));
            }
            var productCategory = await _productCategoryRepository.GetByIdAsync(productDto.CategoryID);
            if (productCategory == null)
            {
                return NotFound(new ApiErrorResult<CreateUpdateProductDto>("Category Id must be exist in system"));
            }

            // MAPPING AND CREATE
            var product = _mapper.Map<Domain.Entities.Product>(productDto);
            await _productRepository.CreateAsync(product);
            var result = _mapper.Map<ProductDto>(product);

            // RESPONSE
            return Ok(new ApiSuccessResult<ProductDto>(result, "Create product is successful !"));
        }
        catch (Exception)
        {

            var responseApiError = new ApiErrorResult<PagedList<ProductDto>>("Error Ocurs !");
            return NotFound(responseApiError);
        }
    }

    [HttpGet("paging")]
    public async Task<ActionResult<PagedList<ProductDto>>> GetProductsPaging(string? sortField, int? sortOrder, string? keyword, int pageIndex = 1, int pageSize = 10)
    {
        try
        {
            var pagedList = await _productRepository.GetProductsPagingAsync(sortField, sortOrder, keyword, pageIndex, pageSize);
            if (pagedList == null)
            {
                var responseApiError = new ApiErrorResult<PagedList<ProductDto>>("Error Ocurs !");
                return NotFound(responseApiError);
            }

            var responseApiSuccess = new ApiSuccessResult<PagedList<ProductDto>>(pagedList, "get products paging is successful !");
            return Ok(responseApiSuccess);
        }
        catch (Exception)
        {
            var responseApiError = new ApiErrorResult<PagedList<ProductDto>>("Error Ocurs !");
            return NotFound(responseApiError);
        }
    }


    [HttpGet("{id:long}")]
    public async Task<ActionResult<ProductDto>> GetProduct([Required] long id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new ApiErrorResult<ProductDto>("get product is failed !"));
            }

            var result = _mapper.Map<ProductDto>(product);
            return Ok(new ApiSuccessResult<ProductDto>(result, "get product is successful !"));
        }
        catch (Exception)
        {
            return BadRequest(new ApiErrorResult<ProductDto>("Error Ocurs !"));
        }
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct([Required] long id, [FromBody] CreateUpdateProductCategoryDto productDto)
    {
        try
        {
            // CHECKING
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(new ApiErrorResult<PagedList<ProductDto>>("Error Occurs !"));
            }

            // MAPPING AND UPDATE
            var updateProduct = _mapper.Map(productDto, product);
            await _productRepository.UpdateAsync(updateProduct);
            var result = _mapper.Map<ProductDto>(product);

            // RETURN 
            return Ok(new ApiSuccessResult<ProductDto>(result, "Update product is successful !"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiErrorResult<ProductDto>("Error Ocurs !"));
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<ProductDto>> DeleteProduct(long id)
    {
        try
        {
            // CHECKING AND DELETE
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(product);

            // MAPPING AND RETURN
            var result = _mapper.Map<ProductDto>(product);
            return Ok(new ApiSuccessResult<ProductDto>(result, "Delete product is successful !"));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiErrorResult<ProductDto>("Error Ocurs !"));
        }
    }

    [HttpDelete]
    public async Task<ActionResult<long[]>> DeleteMultiProducts(long[] ids)
    {
        // Bắt đầu một transaction
        using (var transaction = await _productRepository.BeginTransactionAsync())
        {
            try
            {
                var products = _productRepository.FindByCondition(x => ids.Contains(x.Id));
                // Thực hiện xóa danh sách sản phẩm
                _productRepository.DeleteList(products);

                // Lưu thay đổi và commit transaction
                await _productRepository.EndTransactionAsync();
                return Ok(new ApiSuccessResult<long[]>(ids, "Delete product is successful !"));
            }
            catch (Exception)
            {
                // Nếu có lỗi, rollback transaction
                await _productRepository.RollbackTransactionAsync();
                return BadRequest(new ApiErrorResult<int>("Error Ocurs !"));
            }
        }
    }

    #endregion

}