using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product;

public class CreateUpdateProductCategoryDto
{
    [StringLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    public string Name { get; set; }
    public IFormFile? Image { get; set; }

    [StringLength(128, ErrorMessage = "SeoAlias cannot exceed 128 characters")]
    [Column(TypeName = "nvarchar(128)")]
    public string? SeoAlias { get; set; }

    [StringLength(128, ErrorMessage = "SeoTitle cannot exceed 128 characters")]
    [Column(TypeName = "nvarchar(128)")]
    public string? SeoTitle { get; set; }

    [StringLength(158, ErrorMessage = "MetaKeywords cannot exceed 158 characters")]
    public string? MetaKeywords { get; set; }

    [StringLength(158, ErrorMessage = "MetaDescription cannot exceed 158 characters")]
    public string? MetaDescription { get; set; }

    public int? ParentID { get; set; }

    public int? SortOrder { get; set; }

    public bool? Visibility { get; set; }
}