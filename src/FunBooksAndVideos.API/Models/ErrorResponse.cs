namespace FunBooksAndVideos.API.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public ErrorResponse(string message, IEnumerable<string> errors = null)
        {
            Message = message;
            Errors = errors ?? new List<string>();
        }
    }
}
