namespace Entities.Exceptions
{
    public sealed class LongUrlNotFoundException : NotFoundException
    {
        public LongUrlNotFoundException(string longUrl)
            : base($"The long url: {longUrl} doesn't exist in the database.")
        {
        }
    }   
}
