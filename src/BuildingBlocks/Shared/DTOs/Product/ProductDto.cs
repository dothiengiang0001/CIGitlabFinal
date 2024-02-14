namespace Shared.DTOs.Product;

public class ProductDto
{
    public long ProductID { get; set; }

    public long? CategoryID { get; set; }

    public string? CategoryName { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double? StarNumber { get; set; }

    public decimal? PromotionPrice { get; set; }

    public decimal? Price { get; set; }

    public string Image { get; set; }

    public string ThumbImage { get; set; }

    public string ImageList { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public long? UserCreated { get; set; }
    public long? UserUpdated { get; set; }
    public string SeoAlias { get; set; }
    public string SeoTitle { get; set; }

    public string MetaKeywords { get; set; }

    public string MetaDescription { get; set; }

    public int? Status { get; set; }

    public string Video { get; set; }

    public int? Warranty { get; set; }

    public int? ViewCount { get; set; }
}