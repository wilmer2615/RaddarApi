namespace Raddar.Api.Business
{
    public interface IURLShortener
    {
        string Encode(string longUrl);
        string Decode(string shortUrl);
    }
}
