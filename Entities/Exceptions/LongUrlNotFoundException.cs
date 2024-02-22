namespace Entities.Exceptions
{
    public sealed class LongUrlNotFoundException : NotFoundException
    {
        public LongUrlNotFoundException(string shortUrl)
            : base($"The short url: {shortUrl} doesn't exist in the database.")
        {
        }
    }   
}
