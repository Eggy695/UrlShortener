namespace Entities.Exceptions
{
    public sealed class NotValidUrlException : BadRequestException
    {
        public NotValidUrlException(string url)
            : base($"The URL: {url} is not a valid URL.")
        {

        }
    }
}
