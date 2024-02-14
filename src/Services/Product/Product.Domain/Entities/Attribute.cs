using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
    public class Attribute
    {
        [Key]
        public long AttrID { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
    }
}
