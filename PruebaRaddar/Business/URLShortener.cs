using Raddar.Api.Business;
using Raddar.Shared;

namespace PruebaRaddar.Business
{
    public class URLShortener : IURLShortener
    {
        private Dictionary<string, string> urlMap;
        private Dictionary<string, string> reverseUrlMap;
        private string chars = BasicConfigurations.AllowedCharacters;  
        private Random random;
        private int shortUrlLength;

        public URLShortener(int shortUrlLength)
        {
            if (shortUrlLength <= 0)
            {
                throw new ArgumentException("El tamaño de la URL corta debe ser mayor que cero.");
            }

            urlMap = new Dictionary<string, string>();
            reverseUrlMap = new Dictionary<string, string>();
            random = new Random();
            this.shortUrlLength = shortUrlLength;
        }

        public string Encode(string longUrl)
        {
            if (reverseUrlMap.ContainsKey(longUrl))
            {
                return reverseUrlMap[longUrl];
            }

            var shortUrl = GenerateShortUrl();
            while (urlMap.ContainsKey(shortUrl))
            {
                shortUrl = GenerateShortUrl();
            }

            urlMap[shortUrl] = longUrl;
            reverseUrlMap[longUrl] = shortUrl;
            return $"{BasicConfigurations.BaseShortUrlResult}{shortUrl}";
        }

        public string Decode(string shortUrl)
        {
            string shortUrlCode = shortUrl.Split('/').Last();

            return urlMap.ContainsKey(shortUrlCode) ? urlMap[shortUrlCode] : null;
        }

        private string GenerateShortUrl()
        {
            var shortUrl = new char[shortUrlLength];
            for (int i = 0; i < shortUrl.Length; i++)
            {
                shortUrl[i] = chars[random.Next(chars.Length)];
            }
            return new string(shortUrl);
        }
    }
}
