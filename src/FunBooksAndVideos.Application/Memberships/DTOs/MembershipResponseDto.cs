namespace FunBooksAndVideos.Application.Memberships.DTOs
{
    public class MembershipResponseDto
    {
        public Guid CustomerId { get; set; }
        public ICollection<string> Memberships { get; set; } = new List<string>();
    }
}
