using FunBooksAndVideos.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunBooksAndVideos.Infrastructure.Persistence.Configurations
{
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.HasKey(poi => poi.Id);

            builder.HasOne(poi => poi.Product)
                   .WithMany()  
                   .OnDelete(DeleteBehavior.Restrict);

            // Relationship: PurchaseOrderItem (Many) - PurchaseOrder (1)
            builder.HasOne(poi => poi.PurchaseOrder)
                   .WithMany(po => po.Items)
                   .HasForeignKey(poi => poi.PurchaseOrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: PurchaseOrderItem (Many) - Product (1) 
            builder.HasOne(poi => poi.Product)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(poi => poi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
