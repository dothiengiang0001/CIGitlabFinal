using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{

    [PrimaryKey("AttrValueID", "ProductID")]
    public class ProductAttribute
    {
        [Key]
        public long AttrValueID { get; set; }

        [Key]
        public long ProductID { get; set; }
    }
}
