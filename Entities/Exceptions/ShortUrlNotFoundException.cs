namespace Entities.Exceptions
{
    public sealed class ShortUrlNotFoundException : NotFoundException
    {
        public ShortUrlNotFoundException(string shortUrl)
            :base($"The short url: {shortUrl} doesn't exist in the database.")
        {
            
        }
    }
}
