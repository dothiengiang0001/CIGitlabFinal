using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductCategory : EntityAuditBase<long>
    {
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Image { get; set; }

        [StringLength(128)]
        [Unicode(false)]
        public string SeoAlias { get; set; }

        [StringLength(128)]
        [Unicode(false)]
        public string SeoTitle { get; set; }

        [StringLength(158)]
        public string MetaKeywords { get; set; }

        [StringLength(158)]
        public string MetaDescription { get; set; }

        public long? ParentID { get; set; }

        public int? SortOrder { get; set; }

        public bool? Visibility { get; set; }
    }
}
