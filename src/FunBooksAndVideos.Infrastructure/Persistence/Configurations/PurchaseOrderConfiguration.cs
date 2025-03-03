using FunBooksAndVideos.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunBooksAndVideos.Infrastructure.Persistence.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(po => po.Id); 
            builder.Property(po => po.TotalAmount).IsRequired();
            builder.Property(po => po.CustomerId).IsRequired();

            // Relationship: Customer (1) - PurchaseOrder (Many)
            builder.HasOne(po => po.Customer)
                .WithMany(c => c.PurchaseOrders)
                .HasForeignKey(po => po.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship: PurchaseOrder (1) - PurchaseOrderItem (Many)
            builder.HasMany(po => po.Items)
                .WithOne(poi => poi.PurchaseOrder)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
