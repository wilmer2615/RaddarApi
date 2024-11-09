using Microsoft.AspNetCore.Mvc;
using Raddar.Api.Business;
using Raddar.Api.Models;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace PruebaRaddar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinyController : ControllerBase
    {
        private readonly IURLShortener _urlShortener;

        public TinyController(IURLShortener urlShortener)
        {
            _urlShortener = urlShortener;
        }

        [HttpPost("encode")]
        public ActionResult<string> Encode([FromBody] LongUrlRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shortUrl = _urlShortener.Encode(request.Url);
            return Ok(shortUrl);
        }

        [HttpPost("decode")]
        public ActionResult<string> Decode([FromBody] URLRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var longUrl = _urlShortener.Decode(request.Url);
            if (longUrl == null)
            {
                return NotFound("No se encontró la URL larga para la URL corta proporcionada.");
            }

            return Ok(longUrl);
        }

        [HttpGet("redirect/{shortUrl}")]
        public IActionResult RedirectToLongUrl(string shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                return BadRequest("La URL corta no puede estar vacía.");
            }

            var decodedShortUrl = HttpUtility.UrlDecode(shortUrl);
            var longUrl = _urlShortener.Decode(decodedShortUrl);
            if (longUrl == null)
            {
                return NotFound("No se encontró la URL larga para la URL corta proporcionada.");
            }

            return Redirect(longUrl);
        }
    }
}
