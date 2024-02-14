using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class Product : EntityAuditBase<long>
    {

        [Required]
        public long CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public ProductCategory Category { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public double? StarNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PromotionPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Price { get; set; }

        [StringLength(128)]
        public string Image { get; set; }

        [StringLength(128)]
        public string ThumbImage { get; set; }

        public string ImageList { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        public long? UserCreated { get; set; }

        public long? UserUpdated { get; set; }

        [StringLength(128)]
        public string SeoAlias { get; set; }

        [StringLength(128)]
        public string SeoTitle { get; set; }

        [StringLength(158)]
        public string MetaKeywords { get; set; }

        [StringLength(158)]
        public string MetaDescription { get; set; }

        public int? Status { get; set; }

        [StringLength(128)]
        public string Video { get; set; }

        public int? Warranty { get; set; }

        public int? ViewCount { get; set; }
    }
}