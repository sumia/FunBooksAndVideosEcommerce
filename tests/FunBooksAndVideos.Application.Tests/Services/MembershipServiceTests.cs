using FluentAssertions;
using FunBooksAndVideos.Application.Memberships.Services;
using FunBooksAndVideos.Domain.Entities.Memberships;
using FunBooksAndVideos.Domain.Enums;
using FunBooksAndVideos.Infrastructure.Persistence.Interfaces;
using Moq;

namespace FunBooksAndVideos.Application.Tests.Services
{
    public class MembershipServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly MembershipService _membershipService;

        public MembershipServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _membershipService = new MembershipService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Test_ShouldActivateMembership_WhenMembershipIsAddedToOrder()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var membershipType = MembershipType.BookClubMembership;

            _unitOfWorkMock
                .Setup(u => u.Memberships.Create(It.IsAny<Membership>()))
                .ReturnsAsync(customerId);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .ReturnsAsync(1);

            var membership = new Membership()
            {
                CustomerId = customerId,
                ProductId = productId,
                MembershipType = membershipType
            };

            // Act
            var result = await _membershipService.ActivateMembership(membership);

            // Assert
            result.Should().BeTrue();
            _unitOfWorkMock.Verify(u => 
                u.Memberships.Create(It.Is<Membership>(m =>
                    m.CustomerId == customerId &&
                    m.MembershipType == membershipType
                )), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);

        }
    }
}
