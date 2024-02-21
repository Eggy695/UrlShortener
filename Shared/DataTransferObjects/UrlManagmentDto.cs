namespace Shared.DataTransferObjects
{
    public record UrlManagmentDto
    {
        public string? OriginalUrl { get; set;}
        public string? ShortUrl { get; set;}
    }        
}
