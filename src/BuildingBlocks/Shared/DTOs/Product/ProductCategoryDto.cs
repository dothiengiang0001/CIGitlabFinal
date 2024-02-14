using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product;

public abstract class ProductCategoryDto
{
    public long CategoryID { get; set; }
    public string Name { get; set; }
    public string SeoAlias { get; set; }
    public string SeoTitle { get; set; }
    public string MetaKeywords { get; set; }
    public string MetaDescription { get; set; }
    public long? ParentID { get; set; }
    public int? SortOrder { get; set; }
    public bool? Visibility { get; set; }
}