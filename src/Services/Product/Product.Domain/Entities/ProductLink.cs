using Contracts.Domains.Interfaces;

namespace Product.Domain.Entities
{
    public class ProductLink : EntityAuditBase<int>
    {
        public long? ProductID { get; set; }

        public long? LinkedProductID { get; set; }
    }
}
