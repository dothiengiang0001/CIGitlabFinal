using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Entities
{
    public class AttributeValue
    {
        [Key]
        public long AttrValueID { get; set; }

        public long? AttrID { get; set; }

        [StringLength(128)]
        public string Name { get; set; }
    }
}
