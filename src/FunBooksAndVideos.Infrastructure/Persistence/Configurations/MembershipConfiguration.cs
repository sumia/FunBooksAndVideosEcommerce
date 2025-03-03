using FunBooksAndVideos.Domain.Entities.Memberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FunBooksAndVideos.Infrastructure.Persistence.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
           
            builder.HasKey(m => m.Id);

            builder.Property(m => m.MembershipType)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(m => m.Customer)
                .WithOne(c => c.Membership)
                .HasForeignKey<Membership>(m => m.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
    