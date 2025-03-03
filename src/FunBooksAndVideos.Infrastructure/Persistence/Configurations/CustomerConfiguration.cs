using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FunBooksAndVideos.Domain.Entities.Customers;
using FunBooksAndVideos.Domain.Entities.Memberships;

namespace FunBooksAndVideos.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(25);


            // Relationship: Customer (1) ↔ Membership (1)
            builder.HasOne(c => c.Membership)
                   .WithOne(m => m.Customer)
                   .HasForeignKey<Membership>(m => m.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
