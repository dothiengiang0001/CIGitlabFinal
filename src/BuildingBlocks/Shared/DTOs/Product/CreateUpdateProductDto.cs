using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product;

public class CreateUpdateProductDto
{
    public long CategoryID { get; set; }

    [StringLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    public string Name { get; set; }

    [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
    public string Description { get; set; }

    [StringLength(128, ErrorMessage = "Image cannot exceed 128 characters")]
    public string Image { get; set; }

    [StringLength(128, ErrorMessage = "ThumbImage cannot exceed 128 characters")]
    public string ThumbImage { get; set; }

    public string ImageList { get; set; }

    [StringLength(128, ErrorMessage = "SeoAlias cannot exceed 128 characters")]
    public string SeoAlias { get; set; }

    [StringLength(128, ErrorMessage = "SeoTitle cannot exceed 128 characters")]
    public string SeoTitle { get; set; }

    [StringLength(158, ErrorMessage = "MetaKeywords cannot exceed 158 characters")]
    public string MetaKeywords { get; set; }

    [StringLength(158, ErrorMessage = "MetaDescription cannot exceed 158 characters")]
    public string MetaDescription { get; set; }

    [StringLength(128, ErrorMessage = "Video cannot exceed 128 characters")]
    public string Video { get; set; }
}